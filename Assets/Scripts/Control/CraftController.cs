using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftController : MonoBehaviour
{   
    [SerializeField] bool is4By4 = true;
    Inventory inventory;
    void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    private void OnEnable() {
        inventory.onInventoryUpdate += VerifyCraft;
    }

    private void OnDisable() {
        inventory.onInventoryUpdate -= VerifyCraft;
    }

    void VerifyCraft(){
        int[] craftIDs;
        string craftCode;
        if(is4By4){
            craftIDs = new int[4];
        } else {
            craftIDs = new int[9];
        }

        for (int i = 0; i < inventory.slots.Count - 1; i++)
        {
            if(inventory.slots[i] == null || inventory.slots[i].item == null){
                craftIDs[i] = 0;
            } else {
                craftIDs[i] = inventory.slots[i].item.id;
            }
        }
        if(is4By4) {
            craftIDs = new int[9]{craftIDs[0], craftIDs[1], 0, craftIDs[2], craftIDs[3], 0, 0, 0, 0};
        }
        craftCode = CraftIDsToCode(craftIDs);
        if(!GM.instance.Crafts.ContainsKey(craftCode)) return;
        bool hasEnoughBlocks = true;
        for (int i = 0; i < inventory.slots.Count - 1; i++)
        {
            if(inventory.slots[i] == null || inventory.slots[i].item == null) continue;
            if(inventory.slots[i].amount - 1 < 0){
                hasEnoughBlocks = false;
                break;
            }
        }
        if(!hasEnoughBlocks) return;
        for (int i = 0; i < inventory.slots.Count - 1; i++)
        {
            if(inventory.slots[i] == null) continue;
            inventory.slots[i].RemoveAmount2(1);
        }

        if(inventory.slots[inventory.slots.Count - 1] == null || inventory.slots[inventory.slots.Count - 1].item == null){
            inventory.SetSlot(inventory.slots.Count - 1, new Slot(GM.instance.Crafts[craftCode], 1));
        } else if(inventory.slots[inventory.slots.Count - 1].item.id != GM.instance.Crafts[craftCode].id){
            return;
        } else if(inventory.slots[inventory.slots.Count - 1].item.id == GM.instance.Crafts[craftCode].id && inventory.slots[inventory.slots.Count - 1].amount + 1 <= inventory.slots[inventory.slots.Count - 1].maxAmount){
            inventory.slots[inventory.slots.Count - 1].AddAmount(1);
            inventory.InventoryHasUpdated();
        }
    }

    private string CraftIDsToCode(int[] craftIDs)
    {
        return craftIDs[0] + "|" + craftIDs[1] + "|" + craftIDs[2] + "|" + craftIDs[3] + "|" + craftIDs[4] + "|" + craftIDs[5] + "|" + craftIDs[6] + "|" + craftIDs[7] + "|" + craftIDs[8];
    }
}

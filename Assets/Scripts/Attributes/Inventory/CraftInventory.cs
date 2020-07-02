using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftInventory : Inventory
{   
    [SerializeField] bool is4By4 = true;
    Inventory inventory;

    int[] craftIDs;
    string craftCode;

    private void OnEnable() {
        onInventoryUpdate += VerifyCraft;
    }

    private void OnDisable() {
        onInventoryUpdate -= VerifyCraft;
    }

    void VerifyCraft()
    {
        if (is4By4)
        {
            craftIDs = new int[4];
        }
        else
        {
            craftIDs = new int[9];
        }

        GenerateCraftIDs();

        if (is4By4)
        {
            craftIDs = new int[9] { craftIDs[0], craftIDs[1], 0, craftIDs[2], craftIDs[3], 0, 0, 0, 0 };
        }

        craftCode = CraftIDsToCode(craftIDs);

        if (!GM.instance.Crafts.ContainsKey(craftCode)) return;
        if (inventory.GetSlots()[inventory.GetSlots().Count - 1] != null && inventory.GetSlots()[inventory.GetSlots().Count - 1].item != null && inventory.GetSlots()[inventory.GetSlots().Count - 1].item.id != GM.instance.Crafts[craftCode].id)
        {
            return;
        }
        SubtractItemsFromInventory();

        if (inventory.GetSlots()[inventory.GetSlots().Count - 1] == null || inventory.GetSlots()[inventory.GetSlots().Count - 1].item == null)
        {
            inventory.SetSlot(inventory.GetSlots().Count - 1, new Slot(GM.instance.Crafts[craftCode], 1));
        }
        else if (inventory.GetSlots()[inventory.GetSlots().Count - 1].item.id == GM.instance.Crafts[craftCode].id && inventory.GetSlots()[inventory.GetSlots().Count - 1].amount + 1 <= inventory.GetSlots()[inventory.GetSlots().Count - 1].maxAmount)
        {
            inventory.GetSlots()[inventory.GetSlots().Count - 1].AddAmount(1);
            inventory.InventoryHasUpdated();
        }
    }

    private void SubtractItemsFromInventory()
    {
        for (int i = 0; i < inventory.GetSlots().Count - 1; i++)
        {
            if (inventory.GetSlots()[i] == null) continue;
            inventory.GetSlots()[i].RemoveAmount(1);
        }
    }

    private void GenerateCraftIDs()
    {
        for (int i = 0; i < inventory.GetSlots().Count - 1; i++)
        {
            if (inventory.GetSlots()[i] == null || inventory.GetSlots()[i].item == null)
            {
                craftIDs[i] = 0;
            }
            else
            {
                craftIDs[i] = inventory.GetSlots()[i].item.id;
            }
        }
    }

    private string CraftIDsToCode(int[] craftIDs)
    {
        return craftIDs[0] + "|" + craftIDs[1] + "|" + craftIDs[2] + "|" + craftIDs[3] + "|" + craftIDs[4] + "|" + craftIDs[5] + "|" + craftIDs[6] + "|" + craftIDs[7] + "|" + craftIDs[8];
    }

    public bool TryCraftItem(Item item){
        if(item == null) return false;

        Slot[] craftSlots = CraftRecipesInventoryUI.GetRecipeSlots(item.craftCode);
        // bool hasEnoughItemsToCraft = true;
        // foreach (Slot craftSlot in craftSlots)
        // {
        //     if(craftSlot.item == null) continue;
        //     // if(craftSlot)
        // }
        return true;
    }
}

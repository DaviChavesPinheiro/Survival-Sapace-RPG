using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftInventory : Inventory
{   
    [SerializeField] bool is4By4 = true;

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
        if (is4By4){
            craftIDs = new int[4];
        } else {
            craftIDs = new int[9];
        }

        GenerateCraftIDs();

        if (is4By4) {
            craftIDs = new int[9] { craftIDs[0], craftIDs[1], 0, craftIDs[2], craftIDs[3], 0, 0, 0, 0 };
        }

        craftCode = CraftIDsToCode(craftIDs);

        if (!GM.instance.Crafts.ContainsKey(craftCode)) return;
        if (slots[slots.Count - 1] != null && slots[slots.Count - 1].item != null && slots[slots.Count - 1].item.id != GM.instance.Crafts[craftCode].id) {
            return;
        }
        SubtractItemsFromInventory();

        if (slots[slots.Count - 1] == null || slots[slots.Count - 1].item == null) {
           SetSlot(slots.Count - 1, new Slot(GM.instance.Crafts[craftCode], 1));
        }
        else if (slots[slots.Count - 1].item.id == GM.instance.Crafts[craftCode].id && slots[slots.Count - 1].amount + 1 <= slots[slots.Count - 1].maxAmount) {
            slots[slots.Count - 1].AddAmount(1);
            InventoryHasUpdated();
        }
    }

    private void SubtractItemsFromInventory()
    {
        for (int i = 0; i < slots.Count - 1; i++) {
            if (slots[i] == null) continue;
            slots[i].RemoveAmount(1);
        }
    }

    private void GenerateCraftIDs()
    {
        for (int i = 0; i < slots.Count - 1; i++) {
            if (slots[i] == null || slots[i].item == null) {
                craftIDs[i] = 0;
            } else {
                craftIDs[i] = slots[i].item.id;
            }
        }
    }

    private string CraftIDsToCode(int[] craftIDs)
    {
        return craftIDs[0] + "|" + craftIDs[1] + "|" + craftIDs[2] + "|" + craftIDs[3] + "|" + craftIDs[4] + "|" + craftIDs[5] + "|" + craftIDs[6] + "|" + craftIDs[7] + "|" + craftIDs[8];
    }

    public bool TryCraftItem(Item item){
        if(item == null || slots[slots.Count - 1].item != null && slots[slots.Count - 1].item.id != item.id || slots[slots.Count - 1].amount + 1 > slots[slots.Count - 1].maxAmount) return false;
        Slot[] craftSlots = CraftRecipesInventoryUI.GetRecipeSlots(item.craftCode);
        craftSlots = ReduceSlots(craftSlots);
        foreach (Slot slot in craftSlots)
        {
            if(!TryRemoveItems(slot.item, slot.amount)) return false;
        }
        
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        foreach (Slot slot in craftSlots)
        {
            playerInventory.RemoveItem(slot.item, slot.amount);
        }

        if (slots[slots.Count - 1] == null || slots[slots.Count - 1].item == null) {
           SetSlot(slots.Count - 1, new Slot(item, 1));
        }
        else if (slots[slots.Count - 1].item.id == item.id && slots[slots.Count - 1].amount + 1 <= slots[slots.Count - 1].maxAmount) {
            slots[slots.Count - 1].AddAmount(1);
            InventoryHasUpdated();
        }

        return true;
    }

    private Slot[] ReduceSlots(Slot[] craftSlots)
    {
        Dictionary<int, Slot> reduceSlots = new Dictionary<int, Slot>();
        foreach (Slot slot in craftSlots)
        {
            if(slot == null || slot.item == null) continue;
            if(reduceSlots.ContainsKey(slot.item.id)){
                reduceSlots[slot.item.id].amount++;
            } else {
                reduceSlots[slot.item.id] = slot;
            }
        }
        return reduceSlots.Values.ToArray();
    }

    public bool TryRemoveItems(Item item, int amount){
        int i = 0;
        foreach (Slot slot in GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().GetSlots())
        {
            if(amount <= 0) return true;
            if(slot == null || slot.item == null || slot.item.id != item.id) continue;
            int excess = amount - slot.amount;
            amount = excess;
            i++;
        }
        return false;
    }
    
}

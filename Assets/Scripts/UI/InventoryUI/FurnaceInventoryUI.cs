using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInventoryUI : InventoryUI
{
    

    public void AddMeltItem(){
        Slot[] slots = inventory.GetSlots().ToArray();
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(slots[0]?.item != null && slots[0].amount < slots[0].maxAmount && Inventory.TryRemoveItems(playerInventory, slots[0].item, 1)){
            playerInventory.RemoveItem(slots[0].item, 1);
            slots[0].AddAmount(1);
            inventory.InventoryHasUpdated();
        }
    }
    public void RemoveMeltItem(){
        Slot[] slots = inventory.GetSlots().ToArray();
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(slots[0]?.item != null && slots[0].amount > 0){
            playerInventory.AddItem(slots[0].item, 1);
            slots[0].RemoveAmount(1);
            inventory.InventoryHasUpdated();
        }
    }
    public void AddFuelItem(){
        Slot[] slots = inventory.GetSlots().ToArray();
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(slots[1]?.item != null && slots[1].amount < slots[1].maxAmount && Inventory.TryRemoveItems(playerInventory, slots[1].item, 1)){
            playerInventory.RemoveItem(slots[1].item, 1);
            slots[1].AddAmount(1);
            inventory.InventoryHasUpdated();
        }
    }
    public void RemoveFuelItem(){
        Slot[] slots = inventory.GetSlots().ToArray();
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(slots[1]?.item != null && slots[1].amount > 0){
            playerInventory.AddItem(slots[1].item, 1);
            slots[1].RemoveAmount(1);
            inventory.InventoryHasUpdated();
        }
    }
}

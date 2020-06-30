using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();

    private void Awake() {
        if(inventory != null){
            inventory.onInventoryUpdate += UpdateInventoryUI;
        }
    }

    void UpdateInventoryUI(){
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            slotsUI[i].SetSlot(inventory.slots[i]);
        }
    }

    public void UpdateInvetory(){
        List<Slot> slots = new List<Slot>();
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slots.Add(slotUI.GetSlot());
        }
        inventory.SetInventory(slots);
        
    }
    public void SetInventory(Inventory inventory){
        if(this.inventory == inventory) return;
        if(this.inventory != null) {
            inventory.onInventoryUpdate -= UpdateInventoryUI;
            ResetAllSlots();
            slotsUI.Clear();
        }
        this.inventory = inventory;

        inventory.onInventoryUpdate += UpdateInventoryUI;
        UpdateInventoryUI();
    }

    private void ResetAllSlots()
    {
        foreach (InventorySlotUI slot in slotsUI)
        {
            slot.SetSlot(new Slot(null, 0));
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform itemsContainer;
    [SerializeField] GameObject slotGameObject;
    [SerializeField] List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();

    public event Action onSwapItems;

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
        
        if(onSwapItems != null) onSwapItems();
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

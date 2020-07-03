using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HootBarUI : InventoryUI
{
    private void Start()
    {
        SetInventory(GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>());
        UpdateSelectedSlot();
    }

    override protected void UpdateInventoryUI(){
        for (int i = 0; i < slotsUI.Count; i++)
        {
            slotsUI[i].SetSlot(inventory.GetSlots()[i]);
        }
    }

    override public void UpdateInvetory(){
        List<Slot> slots = inventory.GetSlots();
        int i = 0;
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slots[i] = slotUI.GetSlot();
            i++;
        }
        inventory.SetInventory(slots);
    }

    override public void SetSlotOnFocusIndex(int slotIndex){
        base.SetSlotOnFocusIndex(slotIndex);
        inventory.SetSlotSelected(slotIndex);
        UpdateSelectedSlot();
    }

    private void UpdateSelectedSlot()
    {
        for (int i = 0; i < slotsUI.Count; i++)
        {
            slotsUI[i].GetComponent<SelectSlot>().SetSelected(i == slotOnFocusIndex);
        }
    }
}

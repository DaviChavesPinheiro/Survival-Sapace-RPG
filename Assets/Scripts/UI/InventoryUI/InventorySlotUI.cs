using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] Slot slot;
    [SerializeField] SlotItemIcon inventoryItemIcon;
    [SerializeField] SlotItemAmount inventoryItemAmount;

    public void SetSlot(Slot slot){
        this.slot = slot;
        UpdateSlot();
    }

    public Slot GetSlot(){
        return slot;
    }

    void UpdateSlot(){
        inventoryItemIcon.SetIcon(slot.item ? slot.item.icon : null);
        inventoryItemAmount.SetAmount(slot.amount);
    }

    public void Subtract(){
        slot.RemoveAmount2(1);
    }
}

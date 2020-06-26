using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] Slot slot;
    [SerializeField] InventoryItemIcon inventoryItemIcon;
    [SerializeField] InventoryItemAmount inventoryItemAmount;

    public void SetSlot(Slot slot){
        this.slot = slot;
        UpdateSlot();
    }

    void UpdateSlot(){
        inventoryItemIcon.SetIcon(slot.item ? slot.item.icon : null);
        inventoryItemAmount.SetAmount(slot.amount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] SlotItemIcon inventoryItemIcon;
    [SerializeField] SlotItemAmount inventoryItemAmount;
    [SerializeField] bool canReceive = true;
    [SerializeField] bool readOnly = false;
    [SerializeField] Image ghostImage;
    Slot slot;
    Slot ghostSlot;
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
        slot.RemoveAmount(1);
    }

    public void OnDrop(GameObject receiveSlotGameObject){
        DragItem receiveDragItem = receiveSlotGameObject.GetComponent<DragItem>();
        if(receiveDragItem == null) return;
        InventorySlotUI receiveInventorySlotUI = receiveDragItem.originalParent.GetComponent<InventorySlotUI>();
        Slot receiveSlot = receiveInventorySlotUI.GetSlot();
        if(receiveSlot == null || receiveSlot.item == null) return;
        if(readOnly || receiveInventorySlotUI.readOnly) return;
        if(Slot.hasEqualItemIDs(slot, receiveSlot) && slot.amount + receiveSlot.amount <= slot.maxAmount){
            slot.AddAmount(receiveSlot.amount);
            UpdateSlot();
            receiveInventorySlotUI.SetSlot(new Slot(null, 0));
        } else {
            if(slot != null && slot.item != null && !receiveInventorySlotUI.canReceive || !canReceive) return;
            receiveInventorySlotUI.SetSlot(slot);
            SetSlot(receiveSlot);
        }
        InventoryUI slotInventoryUI = GetComponentInParent<InventoryUI>();
        InventoryUI receiveSlotInventoryUI = receiveDragItem.originalParent.GetComponentInParent<InventoryUI>();

        slotInventoryUI.UpdateInvetory(); 
        receiveSlotInventoryUI.UpdateInvetory();
    }

    public void SetGhostSlot(Slot ghostSlot){
        this.ghostSlot = ghostSlot;
        if(ghostSlot != null && ghostSlot.item != null){
            ghostImage.sprite = ghostSlot.item.icon;
            ghostImage.enabled = true;
        } else {
            ghostImage.sprite = null;
            ghostImage.enabled = false;
        }
    }
}

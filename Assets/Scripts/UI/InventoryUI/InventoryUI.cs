using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] protected List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();
    protected Inventory inventory;
    protected int slotOnFocusIndex;

    virtual protected void Awake() {
        if(GetComponent<Inventory>()){
            SetInventory(GetComponent<Inventory>());
        }
    }
   
    public void SetInventory(Inventory inventory){
        if(this.inventory == inventory) return;
        if(this.inventory != null) {
            inventory.onInventoryUpdate -= UpdateInventoryUI;
        }
        this.inventory = inventory;

        inventory.onInventoryUpdate += UpdateInventoryUI;
        UpdateInventoryUI();
    }

    virtual protected void UpdateInventoryUI(){
        for (int i = 0; i < inventory.GetSlots().Count; i++)
        {
            slotsUI[i].SetSlot(inventory.GetSlots()[i]);
        }
    }

    virtual public void UpdateInvetory(){
        List<Slot> slots = new List<Slot>();
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slots.Add(slotUI.GetSlot());
        }
        inventory.SetInventory(slots);
    }

    public virtual void SetSlotOnFocusIndex(int slotIndex){
        slotOnFocusIndex = slotIndex;
    }

    public int GetSlotOnFocusIndex(){
        return slotOnFocusIndex;
    }
    public Slot GetSlotOnFocus(){
        return slotsUI[slotOnFocusIndex].GetSlot();
    }
    public Inventory GetInventory(){
        return inventory;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   [SerializeField] int slotsAmount = 40;
    List<Slot> slots = new List<Slot>();
    int slotSelected = 0;


    public event Action onInventoryUpdate;

    private void Awake()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < slotsAmount; i++)
        {
            slots.Add(new Slot(null, 0));
        }
    }

    public int AddItem(Item item, int amount){
        for (int i = 0; i < slots.Count; i++)
        {
            if(amount <= 0) break;
            if(slots[i] == null || slots[i].item == null){
                slots[i] = new Slot(item, 0);
                int excess = slots[i].AddAmount(amount);
                amount = excess;
            } else if(slots[i].item.id == item.id && slots[i].amount < slots[i].item.maxStackItem){
                int excess = slots[i].AddAmount(amount);
                amount = excess;
            }
        }
        if(onInventoryUpdate != null) onInventoryUpdate();
        return amount;
    }

    public int RemoveItem(Item item, int amount){
        for (int i = 0; i < slots.Count; i++){
            if(amount <= 0) break;
            if(slots[i] == null || slots[i].item == null || slots[i].item.id != item.id) continue;
            int excess = slots[i].RemoveAmount(amount);
            amount = excess;
        }
        if(onInventoryUpdate != null) onInventoryUpdate();
        return amount;
    }

    public List<Slot> GetSlots(){
        return slots;
    }

    public void SetSlot(int index, Slot slot){
        slots[index] = slot;
        if(onInventoryUpdate != null) onInventoryUpdate();
    }
    public void SetInventory(List<Slot> slots){
        this.slots = slots;
        if(onInventoryUpdate != null) onInventoryUpdate();
    }
    public void SetSlotSelected(int index){
        slotSelected = index;
    }
    public Item GetActiveItem(){
        return slots[slotSelected].item;
    }
    public void InventoryHasUpdated(){
        if(onInventoryUpdate != null) onInventoryUpdate();
    }
    public void Clear(){
        for (int i = 0; i < slotsAmount; i++)
        {
            slots[i] = new Slot(null, 0);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   [SerializeField] protected int slotsAmount = 40;
    protected List<Slot> slots;
    protected int slotSelected = 0;


    public event Action onInventoryUpdate;

    virtual protected void Awake()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        if(slots != null) return;
        slots = new List<Slot>();
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
        return slots[slotSelected] != null ? slots[slotSelected].item : null;
    }
    public Slot GetActiveSlot(){
        return slots[slotSelected];
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

    public InventorySlotData[] GetData(){
        InventorySlotData[] inventorySlotDatas = new InventorySlotData[slots.Count];
        int i = 0;
        foreach (Slot slot in slots)
        {
            inventorySlotDatas[i].itemID = slot != null && slot.item != null ? slot.item.id : 0;
            inventorySlotDatas[i].amount = slot != null ? slot.amount : 0;
            i++;
        }
        return inventorySlotDatas;
    }

    public void SetData(object state){
        InventorySlotData[] data = (InventorySlotData[])state;
        if(data == null) {
            print(null);
            return;
        }
        List<Slot> slotsData = new List<Slot>(slotsAmount);        
        int k = 0;
        foreach (InventorySlotData slotData in data)
        {
            slotsData.Add(new Slot(GM.instance.items.items[slotData.itemID], slotData.amount));
            k++;
        }
        SetInventory(slotsData);
    }

    [System.Serializable]
    public struct InventorySlotData{
        public int itemID;
        public int amount;
    }
}

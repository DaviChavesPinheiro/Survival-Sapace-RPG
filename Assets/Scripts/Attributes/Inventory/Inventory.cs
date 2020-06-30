using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   [SerializeField] int slotsAmount = 40;
    int slotSelected = 0;

    public List<Slot> slots = new List<Slot>();

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

    public bool Add(Item item, int amount){//trocar pra retornar um int
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].item != null && slots[i].item.id != item.id) continue;

            if(slots[i].item == null){
                slots[i].SetItem(item);
                int excess = slots[i].AddAmount(amount);
                if(excess > 0){
                    Add(item, excess);
                }
                if(onInventoryUpdate != null) onInventoryUpdate();
                return true;
            }

            if(slots[i].item.id == item.id && slots[i].amount < slots[i].item.maxStackItem){
                int excess = slots[i].AddAmount(amount);
                if(excess > 0){
                    Add(item, excess);
                }
                if(onInventoryUpdate != null) onInventoryUpdate();
                return true; //Mudar isso, pois o drop vai ser destruido mesmo q vc nao tenha pegado toda a quantidade
            }
        }

        return false;
    }

    public bool Remove(Item item, int amount){
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].item != null && slots[i].item.id == item.id && slots[i].amount - amount >= 0){
                slots[i].RemoveAmount(amount);
                if(onInventoryUpdate != null) onInventoryUpdate();
                return true;
                 //Mudar isso, pois o drop vai ser destruido mesmo q vc nao tenha pegado toda a quantidade
            }
        }
        return false;
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
}

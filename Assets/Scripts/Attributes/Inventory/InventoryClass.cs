using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClass
{
    int size;
    public Slot[] slots;

    public InventoryClass(int size)
    {
        this.size = size;
        this.slots = new Slot[size];
    }

    public int AddItem(Item item, int amount){
        for (int i = 0; i < slots.Length; i++)
        {
            if(amount <= 0) return 0;
            if(slots[i] == null){
                slots[i] = new Slot(item, 0);
                int excess = slots[i].AddAmount(amount);
                amount = excess;
            } else if(slots[i].item.id == item.id && slots[i].amount < slots[i].item.maxStackItem){
                int excess = slots[i].AddAmount(amount);
                amount = excess;
            }
        }
        return amount;
    }

    public int RemoveItem(Item item, int amount){
        for (int i = 0; i < slots.Length; i++){
            if(amount <= 0) return 0;
            if(slots[i] == null || slots[i].item.id != item.id) continue;
            int excess = slots[i].RemoveAmount2(amount);
            amount = -excess;
        }
        return amount;
    }
}

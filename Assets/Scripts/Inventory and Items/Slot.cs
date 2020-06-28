using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    public Item item = null;
    public int amount = 0;
    public int maxAmount = 64;

    public Slot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
        maxAmount = item ? item.maxStackItem : 64;
    }

    public int AddAmount(int value){
        int excess = Mathf.Max((amount + value) - maxAmount, 0);
        amount = Mathf.Min(amount + value, maxAmount);
        return excess;
    }
    public void RemoveAmount(int value){
        amount = Mathf.Max((amount - value), 0);
        if(amount == 0) {
            item = null;
        }
    }

    public void SetItem(Item item){
        this.item = item;
        maxAmount = item ? item.maxStackItem : 64;
    }

    public static bool hasEqualItemIDs(Slot slot1, Slot slot2){
        if(slot1 == null || slot2 == null) return false;
        if(slot1.item == null || slot2.item == null) return false;
        if(slot1.item.id != slot2.item.id) return false;
        return true;
    }

}

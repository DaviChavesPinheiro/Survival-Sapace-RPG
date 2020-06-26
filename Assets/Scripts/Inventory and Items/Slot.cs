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

    public void SetItem(Item item){
        this.item = item;
        maxAmount = item ? item.maxStackItem : 64;
    }

}

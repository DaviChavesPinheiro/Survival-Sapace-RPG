using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   [SerializeField] int slotsAmount = 40;

    // [SerializeField] List<Item> items = new List<Item>();
    public List<Slot> slots = new List<Slot>();

    public event Action onGetDropItem;

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

    public bool Add(Item item, int amount){
        for (int i = 0; i < slots.Count; i++)
        {
            if(slots[i].item != null && slots[i].item.id != item.id) continue;

            if(slots[i].item == null){
                slots[i].SetItem(item);
                int excess = slots[i].AddAmount(amount);
                if(excess > 0){
                    Add(item, excess);
                }
                if(onGetDropItem != null) onGetDropItem();
                return true;
            }

            if(slots[i].item.id == item.id && slots[i].amount < slots[i].item.maxStackItem){
                int excess = slots[i].AddAmount(amount);
                if(excess > 0){
                    Add(item, excess);
                }
                if(onGetDropItem != null) onGetDropItem();
                return true; //Mudar isso, pois o drop vai ser destruido mesmo q vc nao tenha pegado toda a quantidade
            }
        }

        return false;
    }

}

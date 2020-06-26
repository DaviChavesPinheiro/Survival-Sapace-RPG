using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{   [SerializeField] int maxSpace = 10;
    [SerializeField] List<Item> items = new List<Item>();

    public bool Add(Item item){
        if(items.Count >= maxSpace) return false;

        items.Add(item);
        return true;
    }

    public void Remove(Item item){
        items.Remove(item);
    }
}

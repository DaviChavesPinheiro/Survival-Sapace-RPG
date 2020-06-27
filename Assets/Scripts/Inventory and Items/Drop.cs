﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] int amount = 1;
    private void Awake() {
        // GetComponent<SpriteRenderer>().sprite = item ? item.icon : null;
    }
    public void SetItem(Item item, int amount){
        this.item = item;
        this.amount = amount;
        GetComponent<SpriteRenderer>().sprite = item.icon;
    }
    public Item GetItem(){
        return item;
    }
    public int GetAmout(){
        return amount;
    }
}

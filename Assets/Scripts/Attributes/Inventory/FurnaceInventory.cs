using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInventory : Inventory
{
    int fuelAmount;
    const int timeMelting = 2;
    override protected void Awake(){
        base.Awake();
        onInventoryUpdate += CheckIfCanMelt;
    }

    private void CheckIfCanMelt()
    {
        print("CheckIfCanMelt");
        if(slots[0] == null || slots[0].item == null || !slots[0].item.isMelt) return;
        if(slots[1] == null || slots[1].item == null || !slots[1].item.isFuel && fuelAmount <= 0) return;
        if(slots[2] != null && slots[2].item != null &&  slots[2].amount >= slots[2].item.maxStackItem) return;
        if(slots[2] != null && slots[2].item != null && slots[0].item.meltResult != slots[2].item) return;
        print("Can Melt");
        Melt();
    }

    private void Melt()
    {
        StartCoroutine(Melting());
    }

    IEnumerator Melting(){
        print("Melting " + slots[0].item.name);
        if(fuelAmount <= 0){
            fuelAmount += slots[1].item.fuelPotencial;
            slots[1].RemoveAmount(1);
        }
        yield return new WaitForSeconds(timeMelting);
        if(slots[2].item == null){
            slots[2] = new Slot(slots[0].item.meltResult, 1);
        } else {
            slots[2].AddAmount(1);
        }
        slots[0].RemoveAmount(1);
        fuelAmount -= timeMelting;
        InventoryHasUpdated();
    }
}

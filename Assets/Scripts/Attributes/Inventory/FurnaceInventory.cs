using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInventory : Inventory
{
    int fuelAmount;
    const int timeMelting = 2;
    bool isMelting;
    Coroutine melting;
    override protected void Awake(){
        base.Awake();
        onInventoryUpdate += CheckIfCanMelt;
    }

    private void CheckIfCanMelt()
    {
        if(slots[0] == null || slots[0].item == null || !slots[0].item.isMelt) {
            if(isMelting) StopCoroutine(melting);
            return;
        }
        if(slots[1] == null || slots[1].item == null || !slots[1].item.isFuel && fuelAmount <= 0) return;
        if(slots[2] != null && slots[2].item != null &&  slots[2].amount >= slots[2].item.maxStackItem) return;
        if(slots[2] != null && slots[2].item != null && slots[0].item.meltResult != slots[2].item) return;
        Melt();
    }

    private void Melt()
    {
        print("Melt");
        if(isMelting) {
            print("Already Melting.. Returning");
            return;
        }
        if(fuelAmount <= 0){
            print("Fuelless. Consuming new Fuel Item");
            fuelAmount += slots[1].item.fuelPotencial;
            slots[1].RemoveAmount(1);
            InventoryHasUpdated();
        }
        if(!isMelting) {
            melting = StartCoroutine(Melting());
        }
        
    }

    IEnumerator Melting(){
        isMelting = true;
        print("Melting " + slots[0].item.name + " Started");
        yield return new WaitForSeconds(timeMelting);
        print("Melting " + slots[0].item.name + " Finished");
        isMelting = false;
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

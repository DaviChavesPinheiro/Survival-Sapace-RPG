using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInventory : Inventory
{
    int fuelAmount;
    float timeMelting = 2;
    float currentMeltingTime;

    Slot meltSlot, fuelSlot, resultSlot;
    private bool CheckIfCanMelt()
    {
        if(meltSlot == null || meltSlot.item == null || !meltSlot.item.isMelt) return false;
        if((fuelSlot == null || fuelSlot.item == null || !fuelSlot.item.isFuel) && fuelAmount <= 0) return false;
        if(resultSlot != null && resultSlot.item != null &&  resultSlot.amount >= resultSlot.item.maxStackItem) return false;
        if(resultSlot != null && resultSlot.item != null && meltSlot.item.meltResult != resultSlot.item) return false;
        return true;
    }

    private void FixedUpdate() {
        meltSlot = slots[0];
        fuelSlot = slots[1];
        resultSlot = slots[2];

        if(CheckIfCanMelt()){
            if(fuelAmount <= 0){
                fuelAmount += fuelSlot.item.fuelPotencial;
                fuelSlot.RemoveAmount(1);
                InventoryHasUpdated();
                return;
            }
            currentMeltingTime += Time.fixedDeltaTime;
            if(currentMeltingTime >= timeMelting){
                currentMeltingTime = 0;
                
                if(resultSlot.item == null){
                    slots[2] = new Slot(meltSlot.item.meltResult, 1);
                } else {
                    resultSlot.AddAmount(1);
                }
                meltSlot.RemoveAmount(1);
                fuelAmount -= 1;
                InventoryHasUpdated();
            } 
        }
    }
}

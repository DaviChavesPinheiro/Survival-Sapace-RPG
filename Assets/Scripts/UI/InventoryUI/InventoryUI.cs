﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();
    Inventory inventory;

    private void Awake() {
        if(GetComponent<Inventory>()){
            SetInventory(GetComponent<Inventory>());
        }
    }

    private void OnDisable() {
        RemoveGhostImages();
    }
   
    public void SetInventory(Inventory inventory){
        if(this.inventory == inventory) return;
        if(this.inventory != null) {
            inventory.onInventoryUpdate -= UpdateInventoryUI;
        }
        this.inventory = inventory;

        inventory.onInventoryUpdate += UpdateInventoryUI;
        UpdateInventoryUI();
    }

     private void UpdateInventoryUI(){
        for (int i = 0; i < inventory.GetSlots().Count; i++)
        {
            slotsUI[i].SetSlot(inventory.GetSlots()[i]);
        }
    }

    public void UpdateInvetory(){
        List<Slot> slots = new List<Slot>();
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slots.Add(slotUI.GetSlot());
        }
        inventory.SetInventory(slots);
    }

    public void SetGhostImages(Sprite[] sprites){
        int i = 0;
        foreach (Sprite sprite in sprites)
        {
            slotsUI[i].SetGhostImage(sprite);
            i++;
        }
    }

    public void RemoveGhostImages(){
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slotUI.SetGhostImage(null);
        }
    }
}

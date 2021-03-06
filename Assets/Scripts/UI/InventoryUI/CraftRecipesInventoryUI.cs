﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipesInventoryUI : InventoryUI
{
    [SerializeField] CraftInventoryUI craftInventoryUI;
    [SerializeField] CraftInventoryUI craftTableInventoryUI;
    PanelUIControl panelUIControl;

    bool isCraftTableUIOpen;
    override protected void Awake() {
        base.Awake();
        panelUIControl = FindObjectOfType<PanelUIControl>();
    }

    private void OnEnable() {
        panelUIControl.onPanelsChange += UpdateCraftAbleItems;
    }
    private void onDisable() {
        panelUIControl.onPanelsChange -= UpdateCraftAbleItems;
    }

    private void UpdateCraftAbleItems()
    {
        isCraftTableUIOpen = false;
        
        inventory.Clear();
        if (panelUIControl.GetActivesUIIndex()["rightSlot"] == (int)RightSlot.CraftTableUI) isCraftTableUIOpen = true;

        foreach (Item item in GM.instance.items.items)
        {
            if (item == null) continue;
            if (item.craftCode == "") continue;
            if (!isCraftTableUIOpen && !item.is4By4CraftAble) continue;

            inventory.AddItem(item, 1);
        }
    }

    public override void SetSlotOnFocusIndex(int slotIndex)
    {
        base.SetSlotOnFocusIndex(slotIndex);
        SetGhostSlots(slotIndex);
    }

    private void SetGhostSlots(int slotIndex)
    {
        string craftCode = slotsUI[slotIndex].GetSlot().item.craftCode;
        Slot[] craftRecipeSlots = GetRecipeSlots(craftCode);

        if (isCraftTableUIOpen)
        {
            craftTableInventoryUI.SetGhostImages(craftRecipeSlots);
        }
        else
        {
            craftInventoryUI.SetGhostImages(new Slot[4] { craftRecipeSlots[0], craftRecipeSlots[1], craftRecipeSlots[3], craftRecipeSlots[4] });
        }
    }

    public static Slot[] GetRecipeSlots(string craftCode)
    {
        string[] codeIDs = craftCode.Split('|');
        List<Slot> ghostSlots = new List<Slot>();
        for (int i = 0; i < codeIDs.Length; i++)
        {
            if(codeIDs[i] == "0"){
                ghostSlots.Add(new Slot(null, 0));
                continue;
            }
            ghostSlots.Add(new Slot(GM.instance.items.items[int.Parse(codeIDs[i])], 1));
        }
        return ghostSlots.ToArray();
    }

}

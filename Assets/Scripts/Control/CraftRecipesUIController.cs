using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipesUIController : MonoBehaviour
{
    [SerializeField] CraftInventoryUI craftInventoryUI;
    [SerializeField] CraftInventoryUI craftTableInventoryUI;
    Inventory inventory;
    PanelUIControl panelUIControl;

    private void Awake() {
        inventory = GetComponent<Inventory>();
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
        bool only4By4ItemsCraftables = false;
        
        inventory.Clear();
        if (panelUIControl.GetActivesUIIndex().y == (int)RightSlot.CraftUI) only4By4ItemsCraftables = true;
        foreach (Item item in GM.instance.items.items)
        {
            if (item == null) continue;
            if (item.craftCode == "") continue;
            if (only4By4ItemsCraftables == true && !item.is4By4CraftAble) continue;

            inventory.AddItem(item, 1);
        }
    }

    public void OnSelectSlotToViewReceipe(Slot slot){
        if(slot == null || slot.item == null || slot.item.craftCode == "") return;
        Slot[] recipeImages = GetRecipeSlots(slot.item.craftCode);
        if(panelUIControl.GetActivesUIIndex().y == (int)RightSlot.CraftUI){
            craftInventoryUI.SetGhostImages(new Slot[4]{recipeImages[0], recipeImages[1], recipeImages[3], recipeImages[4]});
        } else if(panelUIControl.GetActivesUIIndex().y == (int)RightSlot.CraftTableUI){
            craftTableInventoryUI.SetGhostImages(recipeImages);
        }
    }

    private Slot[] GetRecipeSlots(string craftCode)
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

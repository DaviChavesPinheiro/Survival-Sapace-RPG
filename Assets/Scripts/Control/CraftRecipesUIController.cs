using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipesUIController : MonoBehaviour
{
    [SerializeField] InventoryUI craftInventoryUI;
    [SerializeField] InventoryUI craftTableInventoryUI;
    Inventory inventory;
    PanelUIControl panelUIControl;
    bool only4By4ItemsCraftables = false;
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
        inventory.Clear();
        if (panelUIControl.GetActivesUIIndex().y != (int)RightSlot.CraftTableUI) only4By4ItemsCraftables = true;
        foreach (Item item in GM.instance.items.items)
        {
            if (item == null) continue;
            if (item.craftCode == "") continue;
            if (only4By4ItemsCraftables == true && !item.is4By4CraftAble) continue;

            inventory.AddItem(item, 1);
        }
        inventory.InventoryHasUpdated();
        only4By4ItemsCraftables = false;
    }

    public void OnSelectSlotToViewReceipe(Slot slot){
        if(slot == null || slot.item == null || slot.item.craftCode == "") return;
        Sprite[] recipeImages = GetRecipeImages(slot.item.craftCode);
        if(panelUIControl.GetActivesUIIndex().y == (int)RightSlot.CraftUI){
            craftInventoryUI.SetGhostImages(new Sprite[4]{recipeImages[0], recipeImages[1], recipeImages[3], recipeImages[4]});
        } else if(panelUIControl.GetActivesUIIndex().y == (int)RightSlot.CraftTableUI){
            craftTableInventoryUI.SetGhostImages(recipeImages);
        }
    }

    private Sprite[] GetRecipeImages(string craftCode)
    {
        string[] codeIDs = craftCode.Split('|');
        List<Sprite> sprites = new List<Sprite>();
        for (int i = 0; i < codeIDs.Length; i++)
        {
            if(codeIDs[i] == "0"){
                sprites.Add(null);
                continue;
            }
            sprites.Add(GM.instance.items.items[int.Parse(codeIDs[i])].icon);
        }
        return sprites.ToArray();
    }
}

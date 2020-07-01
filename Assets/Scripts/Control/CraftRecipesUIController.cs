using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipesUIController : MonoBehaviour
{
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
        only4By4ItemsCraftables = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftInventoryUI : InventoryUI
{
    private void OnDisable()
    {
        RemoveGhostImages();
    }

    public void SetGhostImages(Slot[] ghostSlots)
    {
        int i = 0;
        foreach (Slot slot in ghostSlots)
        {
            slotsUI[i].SetGhostSlot(slot);
            i++;
        }
    }

    public void RemoveGhostImages()
    {
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slotUI.SetGhostSlot(new Slot(null, 0));
        }
    }

    public void OnPressAutoCraftButton(){
        InventoryUI craftRecipesInventoryUI = GameObject.FindGameObjectWithTag("CraftRecipesUI").GetComponent<InventoryUI>();
        print(inventory.GetComponent<CraftInventory>().TryCraftItem(craftRecipesInventoryUI.GetInventory().GetSlots()[craftRecipesInventoryUI.GetSlotOnFocusIndex()].item));
    }
}

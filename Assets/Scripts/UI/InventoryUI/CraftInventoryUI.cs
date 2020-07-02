using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventoryUI : InventoryUI
{
    private void OnDisable()
    {
        RemoveGhostImages();
    }

    public void SetGhostImages(Sprite[] sprites)
    {
        int i = 0;
        foreach (Sprite sprite in sprites)
        {
            slotsUI[i].SetGhostImage(sprite);
            i++;
        }
    }

    public void RemoveGhostImages()
    {
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slotUI.SetGhostImage(null);
        }
    }
}

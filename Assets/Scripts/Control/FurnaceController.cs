using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceController : MonoBehaviour, IInterectable
{
    public void OnInterect()
    {
        print("Im a CraftTable");
        PanelUIControl panelUIControl = FindObjectOfType<PanelUIControl>();
        panelUIControl.SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.FurnaceUI);
        panelUIControl.SetActiveUI(true);
        panelUIControl.SetFurnaceInventory(GetComponent<Inventory>());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour, IInterectable
{
    public void OnInterect()
    {
        print("Im a cheast");
        PanelUIControl panelUIControl = FindObjectOfType<PanelUIControl>();
        panelUIControl.SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.ChestUI);
        panelUIControl.SetActiveUI(true);
        panelUIControl.SetChestInventory(GetComponent<Inventory>());
    }
}

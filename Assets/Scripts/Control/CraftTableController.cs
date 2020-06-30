using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTableController : MonoBehaviour
{
    private void Awake() {
        GetComponent<Interact>().onInteract += Interact;
    }

    void Interact(){
        print("Im a CraftTable");
        PanelUIControl panelUIControl = FindObjectOfType<PanelUIControl>();
        panelUIControl.SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.CraftTableUI);
        panelUIControl.SetActiveUI(true);
        panelUIControl.SetCraftTableInventory(GetComponent<Inventory>());
    }
}

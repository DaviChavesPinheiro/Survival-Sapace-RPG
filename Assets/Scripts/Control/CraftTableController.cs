using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTableController : MonoBehaviour
{
    private void OnEnable() {
        GetComponent<Interact>().onInteract += Interact;
    }

    private void OnDisable() {
        GetComponent<Interact>().onInteract -= Interact;
    }
    void Interact(){
        print("Im a CraftTable");
        PanelUIControl panelUIControl = FindObjectOfType<PanelUIControl>();
        panelUIControl.SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.CraftTableUI);
        panelUIControl.SetActiveUI(true);
        panelUIControl.SetCraftTableInventory(GetComponent<Inventory>());
    }
}

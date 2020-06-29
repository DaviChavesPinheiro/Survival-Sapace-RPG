using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    private void Awake() {
        GetComponent<Interact>().onInteract += Interact;
    }

    void Interact(){
        print("Im a cheast");
        PanelUIControl panelUIControl = FindObjectOfType<PanelUIControl>();
        panelUIControl.SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.ChestUI);
        panelUIControl.SetActiveUI(true);
        panelUIControl.SetChestInventory(GetComponent<Inventory>());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private void Awake() {
        GetComponent<Interact>().onInteract += Interact;
    }

    void Interact(){
        print("Im a cheast");
        FindObjectOfType<PanelUIControl>().SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.ChestUI);
        FindObjectOfType<PanelUIControl>().SetActiveUI(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlot : MonoBehaviour
{
    public int activeChild = 0;

    public void ActiveUI(int child){
        if(activeChild == child) return;
        transform.GetChild(activeChild).gameObject.SetActive(false);
        activeChild = child;
        transform.GetChild(activeChild).gameObject.SetActive(true);
    }
}

public enum RightSlot{
    CraftUI,
    CraftTableUI,
    EquipUI,
    ChestUI,
    FurnaceUI
}

public enum LeftSlot{
    PlayerInventoryUI,
    CraftRecipesUI
}

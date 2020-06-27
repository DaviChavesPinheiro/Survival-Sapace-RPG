using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlot : MonoBehaviour
{
    int activeChild = 0;

    public void ActiveUI(int child){
        if(activeChild == child) return;
        
        transform.GetChild(child).gameObject.SetActive(false);
        activeChild = child;
        transform.GetChild(child).gameObject.SetActive(true);
    }
}

public enum RightSlot{
    CraftUI,
    CraftTableUI,
    EquipUI,
    ChestUI
}

public enum LeftSlot{
    PlayerInventoryUI
}

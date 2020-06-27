using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUIControl : MonoBehaviour
{
    [SerializeField] PanelSlot rightSlot;
    [SerializeField] PanelSlot leftSlot;

    private void OnEnable() {
        rightSlot.ActiveUI((int) RightSlot.CraftUI);
        leftSlot.ActiveUI((int) LeftSlot.PlayerInventoryUI);
    }
}

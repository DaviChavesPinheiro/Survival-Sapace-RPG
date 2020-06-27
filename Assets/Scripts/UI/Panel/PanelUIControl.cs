﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUIControl : MonoBehaviour
{
    [SerializeField] PanelSlot rightSlot;
    [SerializeField] PanelSlot leftSlot;
    [SerializeField] InventoryUI playerInventoryUI;
    [SerializeField] InventoryUI chestInventoryUI;

    public void SetSubPanels(LeftSlot left, RightSlot right) {
        leftSlot.ActiveUI((int) left);
        rightSlot.ActiveUI((int) right);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.CraftUI);
            ToggleUI();
        }
    }

    public void ToggleUI()
    {
        rightSlot.gameObject.SetActive(!rightSlot.gameObject.activeSelf);
        leftSlot.gameObject.SetActive(!leftSlot.gameObject.activeSelf);
    }

    public void SetActiveUI(bool active)
    {
        rightSlot.gameObject.SetActive(active);
        leftSlot.gameObject.SetActive(active);
    }

    public void SetPlayerInventory(Inventory inventory){
        playerInventoryUI.SetInventory(inventory);
    }
    public void SetChestInventory(Inventory inventory){
        chestInventoryUI.SetInventory(inventory);
    }
}

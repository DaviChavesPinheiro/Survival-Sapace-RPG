using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HootBarUI : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    List<InventorySlotUI> hootBar = new List<InventorySlotUI>();
    Inventory inventory;

    private void Awake() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventoryUI.onSwapItems += UpdateHootBar;
        inventory.onGetDropItem += UpdateHootBar;
    }

    private void Start() {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            hootBar.Add(transform.GetChild(i).GetComponent<InventorySlotUI>());
        }
    }

    private void UpdateHootBar()
    {
        for (int i = 0; i < hootBar.Count; i++)
        {
            hootBar[i].SetSlot(inventory.slots[i]);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HootBarUI : MonoBehaviour
{
    [SerializeField] InventoryUI inventoryUI;
    List<Transform> hootBar = new List<Transform>();
    Inventory inventory;
    int hootBarSlotSelected = 0;
    private void Awake() {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        InitializeHootBar();
    }

    private void OnEnable() {
        inventory.onInventoryUpdate += UpdateHootBar;
    }

    private void OnDisable() {
        inventory.onInventoryUpdate -= UpdateHootBar;
    }

    private void Start()
    {
        UpdateSelectedSlot();
    }

    private void InitializeHootBar()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            hootBar.Add(transform.GetChild(i));
        }
    }

    private void UpdateHootBar()
    {
        for (int i = 0; i < hootBar.Count; i++)
        {
            hootBar[i].GetComponent<InventorySlotUI>().SetSlot(inventory.slots[i]);

        }
    }

    public void SetHootBarSlotSelected(int index){
        hootBarSlotSelected = index;
        inventory.SetSlotSelected(hootBarSlotSelected);
        UpdateSelectedSlot();
    }

    private void UpdateSelectedSlot()
    {
        for (int i = 0; i < hootBar.Count; i++)
        {
            hootBar[i].GetComponent<SelectSlot>().SetSelected(i == hootBarSlotSelected);
        }
    }
}

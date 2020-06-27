using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform itemsContainer;
    [SerializeField] GameObject slotGameObject;
    List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();
    bool isInventoryInitialize = false;

    public event Action onSwapItems;

    // private void Awake() {
    //     inventory.onGetDropItem += UpdateInventoryUI;
    // }
    private void InitializeInventoryUI()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            InventorySlotUI inventorySlotUI = Instantiate(slotGameObject, Vector3.zero, Quaternion.identity, itemsContainer).GetComponent<InventorySlotUI>();
            slotsUI.Add(inventorySlotUI);
        }
        isInventoryInitialize = true;
    }

    void UpdateInventoryUI(){
        if(!isInventoryInitialize){
            InitializeInventoryUI();
        }

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            slotsUI[i].SetSlot(inventory.slots[i]);
        }
    }

    public void UpdateInvetory(){
        List<Slot> slots = new List<Slot>();
        foreach (InventorySlotUI slotUI in slotsUI)
        {
            slots.Add(slotUI.GetSlot());
        }
        inventory.SetInventory(slots);
        
        if(onSwapItems != null) onSwapItems();
    }
    public void SetInventory(Inventory inventory){
        if(this.inventory == inventory) {
            print("Equal! " + inventory.gameObject.name);
            return;
        }
        if(this.inventory != null) {
            inventory.onGetDropItem -= UpdateInventoryUI;
            DestroyOldSlots();
            slotsUI.Clear();
            isInventoryInitialize = false;
        }
        this.inventory = inventory;
        if(!isInventoryInitialize){
            InitializeInventoryUI();
        }
        inventory.onGetDropItem += UpdateInventoryUI;
        UpdateInventoryUI();
    }

    private void DestroyOldSlots()
    {
        foreach (InventorySlotUI slot in slotsUI)
        {
            Destroy(slot.gameObject);
        }
    }
}

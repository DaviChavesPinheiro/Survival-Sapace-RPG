using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] Transform itemsContainer;
    [SerializeField] GameObject slotGameObject;

    private void Awake() {
        inventory.onGetDropItem += UpdateInventoryUI;
    }

    private void Start()
    {
        InitializeInventoryUI();
    }

    private void InitializeInventoryUI()
    {
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            Instantiate(slotGameObject, Vector3.zero, Quaternion.identity, itemsContainer);
        }
    }

    void UpdateInventoryUI(){
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            itemsContainer.GetChild(i).GetComponent<InventorySlotUI>().SetSlot(inventory.slots[i]);
        }
    }
}

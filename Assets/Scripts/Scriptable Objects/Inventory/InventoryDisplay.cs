using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] int slots = 40;
    [SerializeField] InventoryObject inventory;
    [SerializeField] GameObject slot;
    GameObject inventoryItemsContainer;

    // Dictionary<ItemObject, InventorySlot> inventoryDict = new Dictionary<ItemObject, InventorySlot>();

    private void Awake() {
        // GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().onGetDropItem += UpdateItems;
        inventoryItemsContainer = gameObject.GetComponentInChildren<GridLayoutGroup>().gameObject;
        CreateSlots();
    }

    private void OnEnable() {
        UpdateItems();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < slots; i++)
        {
            Instantiate(slot, Vector3.zero, Quaternion.identity, inventoryItemsContainer.transform);
        }
    }
    void UpdateItems(){
        for (int i = 0; i < inventory.container.Count; i++)
        {
            GameObject item = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, inventoryItemsContainer.transform.GetChild(i).transform) as GameObject;
            item.transform.SetAsFirstSibling();
            item.GetComponent<RectTransform>().localPosition = Vector3.zero;
            inventoryItemsContainer.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
            // inventoryDict.Add(inventory.container[i].item, inventory.container[i]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject inventoryListItems;

    private void Start() {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            GameObject item = Instantiate(inventory.container[i].item.prefab, Vector3.zero, Quaternion.identity, inventoryListItems.transform.GetChild(i).transform) as GameObject;
            item.transform.SetAsFirstSibling();
            item.GetComponent<RectTransform>().localPosition = Vector3.zero;
            inventoryListItems.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = inventory.container[i].amount.ToString("n0");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DropSystem : MonoBehaviour
{
    [SerializeField] GameObject dropPrefab;
    [SerializeField] List<Item> drops = new List<Item>();

    private void OnEnable() {
        GetComponent<Health>().onDie += Drop;
    }

    private void OnDisable() {
        GetComponent<Health>().onDie -= Drop;
    }

    private void Drop()
    {
        foreach (Item item in drops)
        {
            GameObject dropObj = Instantiate(dropPrefab, transform.position, transform.rotation);
            dropObj.transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + UnityEngine.Random.value, Mathf.FloorToInt(transform.position.y) + UnityEngine.Random.value, transform.position.z);
            dropObj.GetComponent<DropController>().SetItem(item, 1);
            Destroy(dropObj, 60);
        }

        Slot[] slots = GetComponent<Inventory>()?.GetSlots().ToArray();
        foreach (Slot slot in slots)
        {
            if(slot?.item == null) continue;
            GameObject dropObj = Instantiate(dropPrefab, transform.position, transform.rotation);
            dropObj.transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + UnityEngine.Random.value, Mathf.FloorToInt(transform.position.y) + UnityEngine.Random.value, transform.position.z);
            dropObj.GetComponent<DropController>().SetItem(slot.item, slot.amount);
            Destroy(dropObj, 60);
        }
    }
}

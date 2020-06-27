using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Items", menuName = "Inventory/Items", order = 0)]
public class Items : ScriptableObject {
    [SerializeField] List<Item> items = new List<Item>();
}
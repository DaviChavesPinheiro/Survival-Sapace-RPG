using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject {
    [SerializeField] int id;
    [SerializeField] new string name;
    [SerializeField] Sprite icon;
}
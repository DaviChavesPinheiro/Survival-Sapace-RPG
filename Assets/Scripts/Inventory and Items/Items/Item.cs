using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject {
    public int id;
    public new string name;
    public Sprite icon;
    public GameObject prefab;
    public ItemType itemType;
    public int maxStackItem = 64;
}

public enum ItemType{
    none,
    block,
    projectile,
    food
}
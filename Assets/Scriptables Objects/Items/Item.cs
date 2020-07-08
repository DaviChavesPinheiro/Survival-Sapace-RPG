using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Space Survival RPG/Item", order = 0)]
public class Item : ScriptableObject {
    public int id;
    public new string name;
    public Sprite icon;
    public GameObject prefab;
    public ItemType itemType;
    public string craftCode;
    public bool is4By4CraftAble;
    public bool isMelt;
    public bool isFuel;
    public int fuelPotencial;
    public Item meltResult;
    public int maxStackItem = 64;
}

public enum ItemType{
    none,
    block,
    projectile,
    food
}
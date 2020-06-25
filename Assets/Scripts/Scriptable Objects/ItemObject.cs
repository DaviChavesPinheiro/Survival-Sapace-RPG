using UnityEngine;

public enum ItemType
{
    Block,
    Consumable,
    Equipment,
    Default
}

[CreateAssetMenu(fileName = "ItemObject", menuName = "Space Survival RPG/ItemObject", order = 0)]
public abstract class ItemObject : ScriptableObject {
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;

}
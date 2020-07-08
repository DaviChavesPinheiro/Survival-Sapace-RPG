using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Space Survival RPG/Entity", order = 0)]
public class Entity : ScriptableObject {
    public int id;
    public new string name;
    public EntityType itemType;
    public GameObject prefab;
}

public enum EntityType{
    none,
    enemy,
    npc
}
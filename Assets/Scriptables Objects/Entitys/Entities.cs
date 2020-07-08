using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entities", menuName = "Space Survival RPG/Entities", order = 0)]
public class Entities : ScriptableObject {
    public List<Entity> entities = new List<Entity>();
}
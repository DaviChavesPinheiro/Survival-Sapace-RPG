using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class EntitiesController : MonoBehaviour, ISaveable
{
    public List<GameObject> entities = new List<GameObject>();
	public static EntitiesController instance;

	void Awake ()
	{
		instance = this;
	}

    private void Start() {
        for (int i = 0; i < 1 - entities.Count; i++)
        {
            GameObject entity = Instantiate(GM.instance.entities.entities[UnityEngine.Random.Range(0, 3)].prefab, Vector3.zero, Quaternion.identity, transform);
            entities.Add(entity);
        }
    }

    public object CaptureState()
    {
        List<EntityData> entitiesData = new List<EntityData>();
        foreach (GameObject entity in entities)
        {
            EntityData entityData = new EntityData();
            entityData.id = entity.GetComponent<EnemyController>().id;
            entityData.isActive = entity.activeSelf;
            entityData.health = entity.GetComponent<Health>().GetData();
            entityData.movement = entity.GetComponent<Movement>().GetData();
            entitiesData.Add(entityData);
        }
        return entitiesData;
    }

    public void RestoreState(object state)
    {
        List<EntityData> entitiesData = (List<EntityData>)state;
        foreach (EntityData entityData in entitiesData)
        {
            GameObject entity = Instantiate(GM.instance.entities.entities[entityData.id].prefab, Vector3.zero, Quaternion.identity, transform);
            entity.SetActive(entityData.isActive);
            entity.GetComponent<Health>().SetData(entityData.health);
            entity.GetComponent<Movement>().SetData(entityData.movement);
            entities.Add(entity);
        }
    }
}

[System.Serializable]
public struct EntityData {
    public int id;
    public bool isActive;
    public object health;
    public object movement;
}
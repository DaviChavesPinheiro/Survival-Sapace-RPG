using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldElement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    World world;

    public void SetWorld(World world){
        this.world = world;
        text.text = world.name;
    }

    public World GetWorld(){
        return world;
    }

    public void Delete(){
        GetComponentInParent<Worlds>().DeleteWorld(world);
    }

    public void SelectWorld(){
        GetComponentInParent<Worlds>().SelectWorld(world);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;

public class Menu : MonoBehaviour, ISaveable
{
    public static Menu instance;
    List<World> worlds = new List<World>();
    private void Awake() {
        instance = this;
    }

    public List<World> GetWorlds(){
        return worlds;
    }

    public void AddWorld(World world){
        worlds.Add(world);
    }

    public void DeleteWorld(World world){
        worlds.Remove(world);
    }

    public object CaptureState()
    {
        return worlds;
    }

    public void RestoreState(object state)
    {
        worlds = (List<World>)state;
    }
}

[System.Serializable]
public struct World
{
    public string name;
    public string seed;
}
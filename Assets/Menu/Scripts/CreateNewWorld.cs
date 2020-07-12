using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class CreateNewWorld : MonoBehaviour
{
    string worldName = "New World";

    public void SetWorldName(string worldName){
        this.worldName = worldName;
    }

    public void CreateWorld(){
        foreach (World world in Menu.instance.GetWorlds())
        {
            if(world.name == worldName) {
                print("Name Already Exists");
                return;
            }
        }
        World newWorld = new World();
        newWorld.name = worldName;
        newWorld.seed = worldName.GetHashCode().ToString();
        print(newWorld.seed);
        Menu.instance.AddWorld(newWorld);
        print(Menu.instance.GetWorlds().Count);
        SavingWrapper savingWrapper = FindObjectOfType(typeof(SavingWrapper)) as SavingWrapper;
        savingWrapper.Save();
    }
}

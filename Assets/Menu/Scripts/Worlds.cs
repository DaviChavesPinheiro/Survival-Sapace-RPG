using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Worlds : MonoBehaviour
{
    [SerializeField] GameObject worldObject;
    [SerializeField] Transform worldsContainer;

    private void OnEnable()
    {
        RefreshList();
    }

    private void RefreshList()
    {
        foreach (Transform child in worldsContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (World world in Menu.instance.GetWorlds())
        {
            WorldElement worldElement = Instantiate(worldObject, worldsContainer).GetComponent<WorldElement>();
            worldElement.SetWorld(world);
        }
    }

    public void DeleteWorld(World world){
        Menu.instance.DeleteWorld(world);
        (FindObjectOfType(typeof(SavingWrapper)) as SavingWrapper).Save();
        (FindObjectOfType(typeof(SavingWrapper)) as SavingWrapper).Delete(world.name);
        RefreshList();
    }

    public void SelectWorld(World world){
        MainMenuGM.instance.SetWorld(world);
        SceneManager.LoadScene(1);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;

public class GameMenuController : MonoBehaviour
{
    SavingWrapper savingWrapper;
    private void Awake() {
        savingWrapper = FindObjectOfType(typeof(SavingWrapper)) as SavingWrapper;
        gameObject.SetActive(false);
    }

    public void SaveAndQuit(){
        Save();
        SceneManager.LoadScene(0);
    }

    public void Save(){
        savingWrapper.Save();
    }
}

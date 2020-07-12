using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGM : MonoBehaviour
{
    public World world;
    public static MainMenuGM instance;
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetWorld(World world){
        this.world = world;
    }

}

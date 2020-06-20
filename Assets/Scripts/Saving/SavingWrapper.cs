﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.K)){
                GetComponent<SavingSystem>().Save(defaultSaveFile);
            }
            if(Input.GetKeyDown(KeyCode.L)){
                GetComponent<SavingSystem>().Load(defaultSaveFile);
            }
        }
    }
}
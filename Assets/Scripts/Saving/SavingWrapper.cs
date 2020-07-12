using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        [SerializeField] string defaultSaveFile;

        private void Awake()
        {
            if(defaultSaveFile == null || defaultSaveFile == ""){
                defaultSaveFile = MainMenuGM.instance.world.name;
            }
            Load();
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
        public void Delete(string fileName)
        {
            GetComponent<SavingSystem>().Delete(fileName);
        }
    }
}
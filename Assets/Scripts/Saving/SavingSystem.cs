using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile){
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, new SerializableVector3(playerTransform.position));
            }
        }
        public void Load(string saveFile){
            string path = GetPathFromSaveFile(saveFile);
            print("Loading to " + path);
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                Transform playerTransform = GetPlayerTransform();
                BinaryFormatter formatter = new BinaryFormatter();
                SerializableVector3 serializableVector3 = (SerializableVector3)formatter.Deserialize(stream);
                
                playerTransform.position = serializableVector3.ToVector();
            }
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        private string GetPathFromSaveFile(string saveFile){
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
        
    }
}

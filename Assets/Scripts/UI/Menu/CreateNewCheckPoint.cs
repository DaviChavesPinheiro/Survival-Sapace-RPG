using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewCheckPoint : MonoBehaviour
{   
    [SerializeField] CheckPointsMenuController checkPointsMenuController;
    string checkPointName = "CheckPoint";

    public void SetCheckPointName(string checkPointName){
        this.checkPointName = checkPointName;
    }

    public void CreateCheckPoint(){
        print("CreateCheckPoint");
        foreach (CheckPoint checkPoint in checkPointsMenuController.GetCheckPoints())
        {
            if(checkPoint.name == checkPointName) {
                print("Name Already Exists");
                return;
            }
        }
        CheckPoint newCheckPoint = new CheckPoint();
        newCheckPoint.name = checkPointName;
        newCheckPoint.id = checkPointName.GetHashCode();
        newCheckPoint.position = new SerializableVector2((Vector2)GameObject.FindGameObjectWithTag("Player").transform.position);
        newCheckPoint.color = new SerializableVector3(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        checkPointsMenuController.AddCheckPoint(newCheckPoint);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointsMenuController : MonoBehaviour
{
    [SerializeField] GameObject checkPointObject;
    [SerializeField] Transform checkPointsContainer;
    List<CheckPoint> checkPoints = new List<CheckPoint>();
    private void OnEnable()
    {
        RefreshList();
    }

    private void RefreshList()
    {
        foreach (Transform child in checkPointsContainer)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (CheckPoint checkPoint in checkPoints)
        {
            CheckPointsMenuElement checkPointElement = Instantiate(checkPointObject, checkPointsContainer).GetComponent<CheckPointsMenuElement>();
            checkPointElement.SetCheckPoint(checkPoint);
        }
    }

    public void AddCheckPoint(CheckPoint checkPoint){
        checkPoints.Add(checkPoint);
    }

    public void DeleteCheckPoint(CheckPoint checkPoint){
        checkPoints.Remove(checkPoint);
        RefreshList();
    }

    public void ToggleCheckPoint(CheckPoint checkPoint){
        for (int i = 0; i < checkPoints.Count; i++)
        {
            if(checkPoints[i].id == checkPoint.id){
                checkPoints[i] = checkPoint;
            }
        }
    }

    public List<CheckPoint> GetCheckPoints(){
        return checkPoints;
    }

    public void SetData(object data){
        checkPoints = (List<CheckPoint>)data;
    }

    public List<CheckPoint> GetData(){
        return checkPoints;
    }
}

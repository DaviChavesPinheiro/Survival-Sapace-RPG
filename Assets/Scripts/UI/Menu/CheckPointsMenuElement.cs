using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckPointsMenuElement : MonoBehaviour
{
    [SerializeField] Color enableColor;
    [SerializeField] Color disableColor;
    [SerializeField] TextMeshProUGUI positionText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image image;
    CheckPoint checkPoint;
    public void SetCheckPoint(CheckPoint checkPoint){
        this.checkPoint = checkPoint;
        nameText.text = checkPoint.name;
        positionText.text = checkPoint.position.ToVector().ToString();
        Vector3 color = checkPoint.color.ToVector();
        image.color = new Color(color.x, color.y, color.z);
        GetComponent<Image>().color = checkPoint.marked ? enableColor : disableColor;
    }

    public CheckPoint GetCheckPoint(){
        return checkPoint;
    }

    public void Delete(){
        GetComponentInParent<CheckPointsMenuController>().DeleteCheckPoint(checkPoint);
    }

    public void SelectCheckPoint(){
        checkPoint.marked = !checkPoint.marked;
        GetComponent<Image>().color = checkPoint.marked ? enableColor : disableColor;
        GetComponentInParent<CheckPointsMenuController>().ToggleCheckPoint(checkPoint);
    }
}

[System.Serializable]
public struct CheckPoint
{
    public int id;
    public string name;
    public SerializableVector2 position;
    public SerializableVector3 color;
    public bool marked;
}

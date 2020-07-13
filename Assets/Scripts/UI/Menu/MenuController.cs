using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class MenuController : MonoBehaviour, ISaveable
{
    public static MenuController instance;
    [SerializeField] CheckPointsMenuController checkPointsMenuController;
    private void Awake() {
        instance = this;
    }

    public object CaptureState()
    {
        Menu menu = new Menu();
        menu.checkPoints = checkPointsMenuController.GetData();
        return menu;
    }

    public void RestoreState(object state)
    {
        Menu menu = (Menu)state;
        checkPointsMenuController.SetData(menu.checkPoints);
    }

    [System.Serializable]
    struct Menu
    {
        public List<CheckPoint> checkPoints;
    }

}

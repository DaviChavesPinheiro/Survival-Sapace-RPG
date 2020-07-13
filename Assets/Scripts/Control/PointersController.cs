using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointersController : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    Dictionary<Transform, PointerController> pointersDictionary = new Dictionary<Transform, PointerController>();

    public void AddPointer(Transform target, float visibleDistance = Mathf.Infinity){
        if(!pointersDictionary.ContainsKey(target) && target.gameObject.activeSelf){
            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target.position);
            bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
            if(isOffScreen){
                PointerController pointerInstance = CreatePointer(target, visibleDistance); 
                pointersDictionary.Add(target, pointerInstance);
            }
        }
    }

    public PointerController CreatePointer(Transform target, float visibleDistance) {
        PointerController pointerController = Instantiate(pointer, transform).GetComponent<PointerController>();
        pointerController.SetTarget(target);
        pointerController.SetVisibleDistance(visibleDistance);
        return pointerController;
    }

    public void DestroyPointer(Transform target) {
        pointersDictionary.Remove(target);
    }

}

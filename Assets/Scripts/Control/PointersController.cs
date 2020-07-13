using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointersController : MonoBehaviour
{
    [SerializeField] GameObject pointer;
    Dictionary<Transform, PointerController> pointersDictionary = new Dictionary<Transform, PointerController>();

    public void AddPointer(Transform target, Color color, float visibleDistance = Mathf.Infinity){
        if(!pointersDictionary.ContainsKey(target) && target.gameObject.activeSelf){
            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target.position);
            bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
            if(isOffScreen){
                PointerController pointerInstance = CreatePointer(target, color, visibleDistance); 
                pointersDictionary.Add(target, pointerInstance);
            }
        }
    }

    public void AddPointer(Vector2 position, Color color, float visibleDistance = Mathf.Infinity){
        GameObject obj = new GameObject();
        obj.transform.position = position;
        Transform target = obj.transform;
        if(!pointersDictionary.ContainsKey(target) && target.gameObject.activeSelf){
            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target.position);
            bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
            if(isOffScreen){
                PointerController pointerInstance = CreatePointer(target, color, visibleDistance); 
                pointersDictionary.Add(target, pointerInstance);
            }
        }
    }

    public PointerController CreatePointer(Transform target, Color color, float visibleDistance) {
        PointerController pointerController = Instantiate(pointer, transform).GetComponent<PointerController>();
        pointerController.SetTarget(target);
        pointerController.SetVisibleDistance(visibleDistance);
        pointerController.SetColor(color);
        return pointerController;
    }

    public void DestroyPointer(Transform target) {
        pointersDictionary.Remove(target);
    }
    public void DestroyPointer(Vector2 position) {
        
        foreach(KeyValuePair<Transform, PointerController> entry in pointersDictionary)
        {
            if((Vector2)entry.Key.position == position){
                entry.Value.DestroyPointer();
                Destroy(entry.Key.gameObject);
                break;
            }
        }
    }

}

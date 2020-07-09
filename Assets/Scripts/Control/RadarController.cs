using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{
    [SerializeField] float radarSize = 1.5f;
    [SerializeField] LayerMask radarMask;
    [SerializeField] GameObject pointer;
    Dictionary<GameObject, PointerController> pointersDictionary = new Dictionary<GameObject, PointerController>();

    private void Awake() {
        
    }

    private void Update() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Camera.main.orthographicSize * Camera.main.aspect * radarSize, radarMask);
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy" && !pointersDictionary.ContainsKey(collider.gameObject) && collider.gameObject.activeSelf){
                Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(collider.transform.position);
                bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
                if(isOffScreen){
                    PointerController pointerInstance = CreatePointer(collider.transform); 
                    pointersDictionary.Add(collider.gameObject, pointerInstance);
                }
            }
        }
    }

    public PointerController CreatePointer(Transform target) {
        PointerController pointerController = Instantiate(pointer, transform).GetComponent<PointerController>();
        pointerController.SetTarget(target);
        return pointerController;
    }

    public void DestroyPointer(GameObject target) {
        pointersDictionary.Remove(target);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, Camera.main.orthographicSize * Camera.main.aspect * radarSize);
    }

}

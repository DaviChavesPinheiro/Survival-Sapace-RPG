using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCasting : MonoBehaviour
{
    [SerializeField] LayerMask touch;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) {
			foreach(Touch toque in Input.touches){
				Ray ray = Camera.main.ScreenPointToRay(toque.position);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 100, touch)){
					GameObject gameObjectTouched = hit.transform.gameObject;

					switch (toque.phase) {
					case TouchPhase.Stationary:
						gameObjectTouched.SendMessage ("OnTouchStationary", SendMessageOptions.DontRequireReceiver);
						break;
					case TouchPhase.Began:
						gameObjectTouched.SendMessage ("OnTouchBegan", SendMessageOptions.DontRequireReceiver);
						break;
					case TouchPhase.Ended:
						gameObjectTouched.SendMessage ("OnTouchEnded", SendMessageOptions.DontRequireReceiver);
						break;
					case TouchPhase.Canceled:
						gameObjectTouched.SendMessage ("OnTouchCanceled", SendMessageOptions.DontRequireReceiver);
						break;
					}


				}
			}

		}
    }
}

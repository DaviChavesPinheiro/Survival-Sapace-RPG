﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCasting : MonoBehaviour
{
    [SerializeField] LayerMask touch;

    void Update()
    {
        if (Input.touchCount > 0) {
			foreach(Touch toque in Input.touches){
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(toque.position), Vector3.forward, 1000, touch);
				if(hit.collider != null){
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

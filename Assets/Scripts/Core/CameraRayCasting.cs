using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCasting : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction, 100);

            if(raycastHit.collider != null){
                print(raycastHit.collider.gameObject.name);
            }
        }
    }
}

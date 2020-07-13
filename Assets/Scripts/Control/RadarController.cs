using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarController : MonoBehaviour
{
    [SerializeField] float radarSize = 1.5f;
    [SerializeField] LayerMask radarMask;
    PointersController pointersController;
    private void Awake() {
        pointersController = GetComponent<PointersController>();
    }

    private void Update() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Camera.main.orthographicSize * Camera.main.aspect * radarSize, radarMask);
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Enemy"){
                pointersController.AddPointer(collider.transform, Color.red , 30f);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, Camera.main.orthographicSize * Camera.main.aspect * radarSize);
    }

}

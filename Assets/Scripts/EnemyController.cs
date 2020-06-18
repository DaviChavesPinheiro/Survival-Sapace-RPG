using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;

    // Update is called once per frame
    void Update()
    {
        if(DistanceToPlayer() < chaseDistance){
            GetComponent<Movement>().RotateToPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
            GetComponent<Shooter>().Shoot();
        }
    }

    private float DistanceToPlayer(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return Vector2.Distance(player.transform.position, transform.position);
    }
}

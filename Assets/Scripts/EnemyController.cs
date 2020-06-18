using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    GameObject player;
    Health health;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<Health>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!health.isAlive()) return;
        if(InAttackRange()){
            GetComponent<Movement>().RotateToPosition(player.transform.position);
            GetComponent<Shooter>().Shoot();
        }
    }

    private bool InAttackRange(){
        return Vector2.Distance(player.transform.position, transform.position) < chaseDistance;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);        
    }
}

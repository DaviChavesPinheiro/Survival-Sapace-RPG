using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    GameObject player;
    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        GetComponent<Health>().onDie += onEnemyDie;
    }

    private void OnDisable()
    {
        GetComponent<Health>().onDie -= onEnemyDie;
    }

    void Update()
    {
        if(!health.IsAlive()) return;
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

    private void onEnemyDie()
    {
        gameObject.SetActive(false);
    }
}

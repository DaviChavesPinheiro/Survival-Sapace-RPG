﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    public int id;
    [SerializeField] Transform viewRayCast;
    [SerializeField] LayerMask viewLayerMask;
    [SerializeField] float normalViewDistance = 5f;
    [SerializeField] float suspiciusViewDistance = 10f;
    [SerializeField] float attackDistace = 5f;
    GameObject player;
    Health health;
    EnemyMoviment moviment;

    float currentViewDistance;
    
    void Awake()
    {
        health = GetComponent<Health>();
        moviment = GetComponent<EnemyMoviment>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        currentViewDistance = normalViewDistance;
    }

    private void OnEnable()
    {
        GetComponent<Health>().onDie += OnEnemyDie;
    }

    private void OnDisable()
    {
        GetComponent<Health>().onDie -= OnEnemyDie;
    }

    void Update()
    {
        if(!health.IsAlive()) return;
        if(InViewRange()){
            currentViewDistance = suspiciusViewDistance;
            moviment.Accelerate();
            moviment.RotateToPosition(player.transform.position);
            
            if(InAttackRange()){
                if(InView())
                    GetComponent<Shooter>().Shoot();
            }
        } else {
            currentViewDistance = normalViewDistance;
            if(Vector2.Distance(player.transform.position, transform.position) > currentViewDistance * 4)
            {
                OnEnemyDie();
            }
        }
    }

    private bool InViewRange(){
        return Vector2.Distance(player.transform.position, transform.position) < currentViewDistance;
    }
    private bool InAttackRange(){
        return Vector2.Distance(player.transform.position, transform.position) < attackDistace;
    }

    private bool InView(){
        RaycastHit2D[] raycastHits =  Physics2D.RaycastAll(viewRayCast.position, player.transform.position - transform.position, currentViewDistance, viewLayerMask);
        foreach (RaycastHit2D hit in raycastHits)
        {
            if(hit.transform.tag == "Player") return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if(player && InViewRange())
            Gizmos.color = InView() ? Color.red : Color.cyan;
        Gizmos.DrawWireSphere(transform.position, currentViewDistance);        
    }

    private void OnEnemyDie()
    {
        EntitiesController.instance.entities.Remove(gameObject);
        gameObject.SetActive(false);
    }
}

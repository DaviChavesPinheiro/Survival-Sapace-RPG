using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Health health;
    private void Awake() {
        health = GetComponent<Health>();    
    }

    private void OnEnable()
    {
        GetComponent<Health>().onDie += OnDie;
    }

    private void OnDisable()
    {
        GetComponent<Health>().onDie -= OnDie;
    }

    private void OnDie()
    {
        Chunk chunk = transform.GetComponentInParent<Chunk>();
        if(chunk != null){
            chunk.SetBlock(new Vector2(transform.position.x, transform.position.y), 0);
        }
        Destroy(gameObject);
    }

}

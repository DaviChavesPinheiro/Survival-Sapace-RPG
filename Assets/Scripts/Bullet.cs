using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float demage = 5;
    public float velocity = 15;
    public float life_time = 5;
    public float coolDown = 0.5f;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.tag)
        {            
            case "Player":
                
                break;
            default:
                GiveDemage(collider);
                break;
        }
    }

    private void GiveDemage(Collider2D collider)
    {
        Health healthComponent = collider.GetComponent<Health>();

        if (healthComponent)
        {
            healthComponent.TakeDamage(demage);
        }
    }
}

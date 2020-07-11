using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 20f;
    public float health = -1f;

    public event Action onDie;
    public event Action onHealthChange;

    void Start()
    {
        if(health == -1f){
            SetHealth(maxHealth);
        }

        if (health == 0)
        {
            Die();
        }
        
    }

    public void TakeDamage(float damage)
    {
        SetHealth(health - damage);
    }

    public void SetHealth(float value){
        if(IsAlive() || health == -1f){
            health = Mathf.Max(value, 0);
            health = Mathf.Min(health, maxHealth);
            
            if(onHealthChange != null) onHealthChange();
            
            if (health == 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        print(gameObject.name + " Morreu!");
        if(onDie != null) onDie();
    }

    public bool IsAlive(){
        return health > 0;
    }

    public float GetPercetage()
    {
        return 100 * (health / maxHealth);
    }

    public float GetData(){
        return health;
    }
    
    public void SetData(object state){
        SetHealth((float)state);
    }

}

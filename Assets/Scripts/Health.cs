using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] float initialHealth = 20f;
    float health = 20f;

    void Start()
    {
        if(GetComponent<BaseStats>()){
            initialHealth = GetComponent<BaseStats>().GetHealth();        
        }
        health = initialHealth;
    }

    public void TakeDamage(float damage){
        health = Mathf.Max(health - damage, 0);
        if(health == 0)
        {
            Die();
        }
    }

    public float GetPercetage(){
        return 100 * (health / initialHealth);
    }

    private void Die()
    {
        print(gameObject.name + " Morreu!");
        if (gameObject.tag == "Block"){
            Destroy(gameObject);
        }
    }

    public bool isAlive(){
        return health > 0;
    }
    
    public object CaptureState()
    {
        return health;
    }

    public void RestoreState(object state)
    {
        health = (float)state;

        if(health == 0)
        {
            Die();
        }
    }
    
}

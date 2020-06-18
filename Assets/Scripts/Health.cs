using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 20f;

    public void TakeDamage(float damage){
        health = Mathf.Max(health - damage, 0);
        if(health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print(gameObject.name + " Morreu!");
        if (gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    public bool isAlive(){
        return health > 0;
    }
}

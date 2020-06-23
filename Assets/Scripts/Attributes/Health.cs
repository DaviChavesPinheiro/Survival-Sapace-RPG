using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] float maxHealth = 20f;
    float health = -1f;
    [SerializeField] HealthBar healthBar = null;

    public event Action onDie;
    public event Action onHealthChange;

    void Start()
    {
        if(health == -1f){
            SetHealth(maxHealth);
        }
        UpdateHealthBar();
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
        health = Mathf.Max(value, 0);
        health = Mathf.Min(health, maxHealth);
        
        if(onHealthChange != null) onHealthChange();
        
        UpdateHealthBar();
        if (health == 0)
        {
            Die();
        }
    }

    public float GetPercetage()
    {
        return 100 * (health / maxHealth);
    }

    private void Die()
    {
        print(gameObject.name + " Morreu!");
        if(onDie != null)
            onDie();
        if (gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }
        if(gameObject.tag == "Enemy"){
            gameObject.SetActive(false);
        }
    }

    private void UpdateHealthBar(){
        if (healthBar)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetHealth(health);
        }
    }

    public object CaptureState()
    {
        return health;
    }

    public void RestoreState(object state)
    {
        health = (float)state;
    }

}

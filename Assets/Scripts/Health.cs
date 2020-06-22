using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] float initialHealth = 20f;
    [SerializeField] HealthBar healthBar = null;
    float health = 20f;

    void Start()
    {
        if(GetComponent<BaseStats>()){
            initialHealth = GetComponent<BaseStats>().GetStat(Stat.Health);        
        }
        health = initialHealth;
        if(healthBar){
            healthBar.SetMaxHealth(initialHealth);
        }
    }

    public void TakeDamage(GameObject instigator, float damage){
        health = Mathf.Max(health - damage, 0);
        if(healthBar){
            healthBar.SetHealth(health);
        }
        if(health == 0)
        {
            AwardExperience(instigator);
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

    private void AwardExperience(GameObject instigator)
    {
        Experience experience = instigator.GetComponent<Experience>();
        if (experience == null) return;

        experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
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

        if(healthBar){
            healthBar.SetHealth(health);
        }

        if(health == 0)
        {
            Die();
        }
    }
    
}

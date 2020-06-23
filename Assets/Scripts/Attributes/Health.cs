﻿using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] float initialHealth = -1f;
    [SerializeField] HealthBar healthBar = null;
    [SerializeField] float health = -1f;

    public event Action onDie;

    void Start()
    {
        if (initialHealth == -1)
        {
            if (GetComponent<BaseStats>())
            {
                initialHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }
        if (health == -1)
        {
            health = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        if (healthBar)
        {
            healthBar.SetMaxHealth(initialHealth);
            healthBar.SetHealth(health);

        }

        if (health == 0)
        {
            Die();
        }
        
    }

    private void OnEnable()
    {
        GetComponent<BaseStats>().onLevelUp += onLevelUp;
    }

    private void OnDisable()
    {
        GetComponent<BaseStats>().onLevelUp -= onLevelUp;
    }

    public void TakeDamage(GameObject instigator, float damage)
    {
        health = Mathf.Max(health - damage, 0);
        if (healthBar)
        {
            healthBar.SetHealth(health);
        }
        if (health == 0)
        {
            AwardExperience(instigator);
            Die();
        }
    }

    public float GetPercetage()
    {
        return 100 * (health / initialHealth);
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

    private void onLevelUp()
    {
        health = initialHealth;
    }

    private void AwardExperience(GameObject instigator)
    {
        Experience experience = instigator.GetComponent<Experience>();
        if (experience == null) return;
        if (!GetComponent<BaseStats>()) return;

        experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
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

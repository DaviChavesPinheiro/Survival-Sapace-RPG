using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : StatusBar
{
    Health health;

    private void Awake() {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Start() {
		SetMaxHealth(health.maxHealth);
		SetHealth(health.health);
	}

    private void OnEnable() {
		health.onHealthChange += OnHealthChange;
	}

	private void OnDisable() {
		health.onHealthChange -= OnHealthChange;
	}

    private void OnHealthChange()
    {
        SetHealth(health.health);
    }
}

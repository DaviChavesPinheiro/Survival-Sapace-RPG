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
		SetMaxStatusValue(health.maxHealth);
		SetStatusValue(health.health);
	}

    private void OnEnable() {
		health.onHealthChange += OnHealthChange;
	}

	private void OnDisable() {
		health.onHealthChange -= OnHealthChange;
	}

    private void OnHealthChange()
    {
        SetStatusValue(health.health);
    }
}

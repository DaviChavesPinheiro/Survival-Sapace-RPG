using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
	[SerializeField] Gradient gradient;
	[SerializeField] Image fill;
	Health health;
	private void Awake() {
		health = GetComponentInParent<Health>();
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

    public void SetMaxHealth(float value)
	{
		slider.maxValue = value;
	}

    public void SetHealth(float value)
	{
		slider.value = value;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}

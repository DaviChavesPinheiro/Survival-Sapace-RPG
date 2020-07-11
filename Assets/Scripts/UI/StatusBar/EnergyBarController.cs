using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarController : StatusBar
{
    Energy energy;

    private void Awake() {
        energy = GameObject.FindGameObjectWithTag("Player").GetComponent<Energy>();
    }

    private void Start() {
		SetMaxStatusValue(energy.maxEnergy);
		SetStatusValue(energy.energy);
	}

    private void OnEnable() {
		energy.onEnergyChange += OnEnergyChange;
	}

	private void OnDisable() {
		energy.onEnergyChange -= OnEnergyChange;
	}

    private void OnEnergyChange()
    {
        SetStatusValue(energy.energy);
    }
}

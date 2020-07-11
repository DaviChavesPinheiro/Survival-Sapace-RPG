using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] float energyLossSpeed = 1;
    public float maxEnergy = 20f;
    public float energy = -1f;
    Health health;

    public event Action onEndEnergy;
    public event Action onEnergyChange;

    private void Awake() {
        health = GetComponent<Health>();
    }

    void Start()
    {
        if(energy == -1f){
            SetEnergy(maxEnergy);
        }

        if (energy == 0)
        {
            EndEnergy();
        }
        
    }

    private void Update() {
        TakeEnergy(energyLossSpeed / 10f * Time.deltaTime);
        if(energy/maxEnergy > .95f){
            health?.GainHealth(4 * Time.deltaTime);
        }
    }

    public void TakeEnergy(float value)
    {
        SetEnergy(energy - value);
    }

    public void GainEnergy(float value)
    {
        SetEnergy(energy + value);
    }

    public void SetEnergy(float value){
        energy = Mathf.Max(value, 0);
        energy = Mathf.Min(energy, maxEnergy);
        
        if(onEnergyChange != null) onEnergyChange();
        
        if (energy == 0)
        {
            EndEnergy();
        }
    }

    private void EndEnergy()
    {
        if(onEndEnergy != null) onEndEnergy();
        print(gameObject.name + " EndEnergy!");
        
        health?.TakeDamage(4 * Time.deltaTime);
    }

    public bool IsNotEmpty(){
        return energy > 0;
    }

    public float GetPercetage()
    {
        return 100 * (energy / maxEnergy);
    }

    public float GetData(){
        return energy;
    }
    
    public void SetData(object state){
        if(state != null)
            SetEnergy((float)state);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] float energyLossSpeed = 1;
    public float maxEnergy = 20f;
    public float energy = -1f;

    public event Action onEndEnergy;
    public event Action onEnergyChange;

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
        Health health = GetComponent<Health>();
        if(health){
            health.TakeDamage(4 * Time.deltaTime);
        }
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
        SetEnergy((float)state);
    }
}

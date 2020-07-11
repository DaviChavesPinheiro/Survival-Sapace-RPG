using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consume : MonoBehaviour
{
    [SerializeField] float coolDown = 0.5f;
    Energy energy;
    Inventory inventory;

    float timeSinceLastConsume = Mathf.Infinity;
    private void Awake() {
        inventory = GetComponent<Inventory>();
        energy = GetComponent<Energy>();
    }

     void Update()
    {
        timeSinceLastConsume += Time.deltaTime;
    }

    public void ConsumeItem(){
        if(timeSinceLastConsume < coolDown) return;
        Slot activeSlot = inventory.GetActiveSlot();
        if(activeSlot?.item?.itemType == ItemType.food){
            energy.GainEnergy(activeSlot.item.energyRestoration);
            activeSlot.RemoveAmount(1);
            inventory.InventoryHasUpdated();
        }
        timeSinceLastConsume = 0;
    }
}

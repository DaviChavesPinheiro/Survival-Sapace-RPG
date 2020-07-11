using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class PlayerController : MonoBehaviour, ISaveable
{
    [SerializeField] Joystick joystick;
    Health health;
    Energy energy;
    Movement movement;
    Shooter shooter;
    Inventory inventory;
    Consume consume;
    Transform interact;
    void Awake()
    {
        health = GetComponent<Health>();
        energy = GetComponent<Energy>();
        shooter = GetComponent<Shooter>();
        consume = GetComponent<Consume>();
        movement = GetComponent<Movement>();
        inventory = GetComponent<Inventory>();
        interact = transform.Find("Interact");
    }

    private void Start() {
        // inventory.AddItem(GM.instance.items.items[1], 64 * 12);
        inventory.AddItem(GM.instance.items.items[4], 5);
        inventory.AddItem(GM.instance.items.items[8], 5);
        // inventory.AddItem(GM.instance.items.items[7], 32);
        FindObjectOfType<PanelUIControl>().SetPlayerInventory(inventory);
    }

    private void OnEnable()
    {
        GetComponent<Health>().onDie += OnPlayerDie;
    }

    private void OnDisable()
    {
        GetComponent<Health>().onDie -= OnPlayerDie;
    }

    void Update()
    {
        if (!health.IsAlive()) return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        if (!health.IsAlive()) return;
        Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        if(input.magnitude == 0){
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
        movement.Rotate(input);
        if(Input.GetButton("space")){
            movement.isAccelerating = true;
            movement.Accelerate();
        } else {
            movement.isAccelerating = false;
        }
    }

    private void OnPlayerDie()
    {
        print("GAME OVER");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag)
        {
            case "Drop":
                DropController drop = other.GetComponent<DropController>();
                if(!drop) return;

                int excess = inventory.AddItem(drop.GetItem(), drop.GetAmout());
                if(excess <= 0){
                    Destroy(other.gameObject);
                }
                break;
        }
    }

    public void Interact(){
        Collider2D[] colliders = Physics2D.OverlapBoxAll(interact.position, new Vector2(1, 1), 0);
        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<IInterectable>() != null){
                collider.GetComponent<IInterectable>().OnInterect();
            }
        }
    }

    public void UseItem(){
        print(inventory.GetActiveSlot().item);
        if(inventory.GetActiveSlot()?.item?.itemType == ItemType.food && energy.energy < energy.maxEnergy * 0.95f){
            consume.ConsumeItem();
        } else {
            print(2);
            shooter.Shoot();
        }
    }

    public object CaptureState()
    {
        PlayerData data = new PlayerData();
        data.inventory = GetComponent<Inventory>().GetData();
        data.health = GetComponent<Health>().GetData();
        data.energy = GetComponent<Energy>().GetData();
        data.movement = GetComponent<Movement>().GetData();
        return data;
    }

    public void RestoreState(object state)
    {
        PlayerData data = (PlayerData)state;
        GetComponent<Inventory>().SetData(data.inventory);
        GetComponent<Health>().SetData(data.health);
        GetComponent<Energy>().SetData(data.energy);
        GetComponent<Movement>().SetData(data.movement);
    }
    [System.Serializable]
    struct PlayerData {
        public object inventory;
        public object health;
        public object energy;
        public object movement;
    }
}

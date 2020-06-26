using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] InventoryObject inventory;
    [SerializeField] Joystick joystick;
    Health health;
    Movement movement;
    Inventory inventory;
    bool isPlayerAlive = true;

    public event Action onGetDropItem;

    void Awake()
    {
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
        inventory = GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        GetComponent<Health>().onDie += onPlayerDie;
    }

    private void OnDisable()
    {
        GetComponent<Health>().onDie -= onPlayerDie;
    }

    void Update()
    {
        if (!isPlayerAlive) return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Shooter>().Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!isPlayerAlive) return;
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

    private void onPlayerDie()
    {
        isPlayerAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch (other.tag)
        {
            case "Drop":
                Drop drop = other.GetComponent<Drop>();
                if(!drop) return;

                bool wasPickedUp = inventory.Add(drop.GetItem());
                if(wasPickedUp){
                    if(onGetDropItem != null) onGetDropItem();
                    Destroy(other.gameObject);
                }
                break;
        }
    }
}

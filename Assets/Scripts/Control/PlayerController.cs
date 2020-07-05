﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class PlayerController : MonoBehaviour, ISaveable
{
    [SerializeField] Joystick joystick;
    Health health;
    Movement movement;
    Inventory inventory;
    Transform interact;
    void Awake()
    {
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
        inventory = GetComponent<Inventory>();
        interact = transform.Find("Interact");
    }

    private void Start() {
        // inventory.AddItem(GM.instance.items.items[1], 64 * 12);
        // inventory.AddItem(GM.instance.items.items[8], 5);
        // inventory.AddItem(GM.instance.items.items[2], 32);
        // inventory.AddItem(GM.instance.items.items[7], 32);
        FindObjectOfType<PanelUIControl>().SetPlayerInventory(inventory);
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
        if (!health.IsAlive()) return;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponent<Shooter>().Shoot();
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

    private void onPlayerDie()
    {
        print("GAME OVER");
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

    public object CaptureState()
    {
        PlayerData data = new PlayerData();
        data.inventory = GetComponent<Inventory>().GetData();
        return data;
    }

    public void RestoreState(object state)
    {
        print("Player RestoreState");
        PlayerData data = (PlayerData)state;
        GetComponent<Inventory>().SetData(data.inventory);
    }
    [System.Serializable]
    struct PlayerData {
        public object inventory;
    }
}

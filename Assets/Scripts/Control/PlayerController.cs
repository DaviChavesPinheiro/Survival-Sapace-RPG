using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    Health health;
    Movement movement;
    Inventory inventory;
    bool isPlayerAlive = true;
    Transform interact;
    void Awake()
    {
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
        inventory = GetComponent<Inventory>();
        interact = transform.Find("Interact");
        
    }

    private void Start() {
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
        if (!isPlayerAlive) return;
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
                DropController drop = other.GetComponent<DropController>();
                if(!drop) return;

                bool wasPickedUp = inventory.Add(drop.GetItem(), drop.GetAmout());
                if(wasPickedUp){
                    Destroy(other.gameObject);
                }
                break;
        }
    }

    public void Interact(){
        Collider2D[] colliders = Physics2D.OverlapBoxAll(interact.position, new Vector2(1, 1), 0);
        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<Interact>() != null){
                collider.GetComponent<Interact>().interact();
            }
        }
    }
}

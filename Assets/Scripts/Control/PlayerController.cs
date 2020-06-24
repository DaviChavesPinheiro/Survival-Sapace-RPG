using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    Health health;
    bool isPlayerAlive = true;

    void Awake()
    {
        health = GetComponent<Health>();
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
        // if (Input.GetMouseButton(0))
        // {
        //     GetComponent<Shooter>().Shoot();
        // }
    }

    void FixedUpdate()
    {
        if (!isPlayerAlive) return;
        // Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector2 input = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        GetComponent<Movement>().Rotate(input);

        // GetComponent<Movement>().Accelerate(Input.GetButton("space"));
    }

    private void onPlayerDie()
    {
        isPlayerAlive = false;
    }
}

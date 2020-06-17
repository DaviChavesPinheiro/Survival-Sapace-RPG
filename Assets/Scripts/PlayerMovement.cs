﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // public SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] int maxSpeed = 15;
    [SerializeField] int force = 250;
    [SerializeField] float rotation_speed = 3;
    [SerializeField] float maxLinearDrag = 1;
    [SerializeField] float minLinearDrag = 0;
    [SerializeField] float maxAngularDrag = 1;
    [SerializeField] float minAngularDrag = 0;

    float lerpAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lerpAngle = 360 - transform.rotation.z;
    }

    void FixedUpdate() {
        
        Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        float magnitude = input.magnitude;
        float angle = 360 - Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg;
        lerpAngle = Mathf.LerpAngle(lerpAngle, angle, rotation_speed * Time.fixedDeltaTime * magnitude);

        rb.MoveRotation(lerpAngle);

        rb.AddForce (input * force * Time.fixedDeltaTime);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed || Mathf.Abs(rb.velocity.y) > maxSpeed) {
			rb.drag = 10;
		} else {
			if (magnitude != 0) {
				rb.drag = maxLinearDrag;
				rb.angularDrag = maxAngularDrag;
			} else {
				rb.drag = minLinearDrag;
				rb.angularDrag = minAngularDrag;
			}
		}

        if(magnitude != 0){
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }

    }
}
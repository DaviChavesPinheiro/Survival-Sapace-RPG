﻿using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] TrailRenderer[] trails;

    [SerializeField] protected int maxSpeed = 15;
    [SerializeField] int force = 250;
    [SerializeField] float rotation_speed = 3;
    [SerializeField] float maxLinearDrag = 1;
    [SerializeField] float minLinearDrag = 0;
    [SerializeField] float maxAngularDrag = 1;
    [SerializeField] float minAngularDrag = 0;
    public bool isAccelerating = false;
    float lerpAngle;

    virtual protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trails = GetComponentsInChildren<TrailRenderer>();
    }

    private void Start() {
        lerpAngle = 360 - transform.rotation.z;
    }

    private void Update() {
        if (isAccelerating)
        {
            rb.drag = maxLinearDrag;
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.drag = 10;
            }
        }
        else
        {
            rb.drag = minLinearDrag;
        }
        UpdateTrails();
        isAccelerating = false;
    }

    public void Rotate(Vector2 direction)
    {
        bool isRotating = direction.magnitude > 0;

        if (isRotating)
        {
            float angle = 360 - Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            lerpAngle = Mathf.LerpAngle(lerpAngle, angle, rotation_speed * Time.fixedDeltaTime * direction.magnitude);
            rb.MoveRotation(lerpAngle);
            rb.angularDrag = maxAngularDrag;
        }
        else
        {
            rb.angularDrag = minAngularDrag;
        }
    }

    virtual public void Accelerate()
    {
        if (rb.velocity.magnitude > maxSpeed) return;
        rb.AddForce(transform.up * force * Time.fixedDeltaTime);
    }

    private void UpdateTrails()
    {
        foreach (TrailRenderer trail in trails)
        {
            trail.emitting = isAccelerating;
        }
    }

    public void RotateToPosition(Vector2 position){
        Rotate((position - new Vector2(transform.position.x, transform.position.y)).normalized);
    }
    
    public MovementSaveData GetData(){
        MovementSaveData data = new MovementSaveData();
        data.position = new SerializableVector3(transform.position);
        data.rotation = new SerializableVector3(transform.eulerAngles);
        data.acceleration = new SerializableVector3(new Vector3(rb.velocity.x, rb.velocity.y, 0));
        return data;
    }
    public void SetData(object state){
        MovementSaveData data = (MovementSaveData)state;
        transform.position = data.position.ToVector();
        transform.eulerAngles = data.rotation.ToVector();
        GetComponent<Rigidbody2D>().velocity = new Vector2(data.acceleration.ToVector().x, data.acceleration.ToVector().y);
    }

    [System.Serializable]
    public struct MovementSaveData{
        public SerializableVector3 position;
        public SerializableVector3 rotation;
        public SerializableVector3 acceleration;
    }
}

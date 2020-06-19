using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, ISaveable
{
    Rigidbody2D rb;

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
        lerpAngle = 360 - transform.rotation.z;
    }

    public void Rotate(Vector2 direction)
    {
        float angle = 360 - Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        lerpAngle = Mathf.LerpAngle(lerpAngle, angle, rotation_speed * Time.fixedDeltaTime * direction.magnitude);
        rb.MoveRotation(lerpAngle);

        bool isRotating = direction.magnitude > 0 ? true : false;
        if (isRotating)
        {
            rb.angularDrag = maxAngularDrag;
        }
        else
        {
            rb.angularDrag = minAngularDrag;
        }
    }

    public void Accelerate(bool isAccelerating){
        if (isAccelerating)
        {
            if (Mathf.Abs(rb.velocity.x) > maxSpeed || Mathf.Abs(rb.velocity.y) > maxSpeed)
            {
                rb.drag = 10;
            } else {
                rb.AddForce(transform.up * force * Time.fixedDeltaTime);
                rb.drag = maxLinearDrag;
            }
        }
        else
        {
            rb.drag = minLinearDrag;
        }
    }

    public void RotateToPosition(Vector2 position){
        Rotate((position - new Vector2(transform.position.x, transform.position.y)).normalized);
    }

    public void RotateToPosition(Vector3 position){
        Rotate((new Vector2(position.x, position.y) - new Vector2(transform.position.x, transform.position.y)).normalized);
    }

    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3)state;
        transform.position = position.ToVector();
    }
}

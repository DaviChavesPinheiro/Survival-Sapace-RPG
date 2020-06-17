using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // public SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    [SerializeField] int maxSpeed = 15;
    [SerializeField] int force = 250;
    [SerializeField] float rotation_speed = 3;
    [SerializeField] float maxLinearDrag = 1;
    [SerializeField] float minLinearDrag = 0;
    [SerializeField] float maxAngularDrag = 1;
    [SerializeField] float minAngularDrag = 0;

    float lerpAngle;
    bool isAccelerating;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lerpAngle = 360 - transform.rotation.z;
    }

    public void Move(Vector2 direction){
        isAccelerating = direction.magnitude > 0 ? true : false;
        float angle = 360 - Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg;
        lerpAngle = Mathf.LerpAngle(lerpAngle, angle, rotation_speed * Time.fixedDeltaTime * direction.magnitude);

        rb.MoveRotation(lerpAngle);

        rb.AddForce(direction * force * Time.fixedDeltaTime);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed || Mathf.Abs(rb.velocity.y) > maxSpeed)
        {
            rb.drag = 10;
        }
        else
        {
            if (isAccelerating)
            {
                rb.drag = maxLinearDrag;
                rb.angularDrag = maxAngularDrag;
            }
            else
            {
                rb.drag = minLinearDrag;
                rb.angularDrag = minAngularDrag;
            }
        }
    }

}

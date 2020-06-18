using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!health.isAlive()) return;
        if(Input.GetMouseButton(0)){
            GetComponent<Shooter>().Shoot();
        }
    }

    void FixedUpdate()
    {
        if(!health.isAlive()) return;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        GetComponent<Movement>().Rotate(input);

        GetComponent<Movement>().Accelerate(Input.GetButton("space"));
    }
}

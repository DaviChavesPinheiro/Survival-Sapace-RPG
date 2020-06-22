using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Health health;
    Text healthText;
    void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        healthText = GetComponent<Text>();
    }

    void Update()
    {
        healthText.text = String.Format("{0:0}%", health.GetPercetage());
    }
}

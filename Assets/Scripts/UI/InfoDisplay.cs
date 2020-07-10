using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    Transform player;
    Text text;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = (new Vector2(Mathf.FloorToInt(player.position.x), Mathf.FloorToInt(player.position.y))).ToString();
    }
}

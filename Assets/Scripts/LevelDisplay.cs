using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    BaseStats baseStats;
    Text baseStatsText;

    void Awake()
    {
        baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        baseStatsText = GetComponent<Text>();
    }

    void Update()
    {
        baseStatsText.text = String.Format("{0:0}", baseStats.GetLevel());
    }
}

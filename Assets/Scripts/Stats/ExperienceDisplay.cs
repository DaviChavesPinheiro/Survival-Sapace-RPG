using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    Experience experience;
    Text experienceText;

    void Awake()
    {
        experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        experienceText = GetComponent<Text>();
    }

    void Update()
    {
        experienceText.text = String.Format("{0:0}", experience.GetPoints());
    }
}

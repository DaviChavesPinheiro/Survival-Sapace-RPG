using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Slider sliderBar;
    
    protected void SetMaxStatusValue(float value)
	{
		sliderBar.maxValue = value;
	}

    protected void SetStatusValue(float value)
	{
		sliderBar.value = value;
	}
}

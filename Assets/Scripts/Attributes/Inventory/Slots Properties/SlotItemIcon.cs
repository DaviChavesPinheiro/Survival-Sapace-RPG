using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemIcon : MonoBehaviour
{
    public void SetIcon(Sprite icon)
    {
        Image iconImage = GetComponent<Image>();
        if (icon == null)
        {
            iconImage.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = icon;
        }
    }
}

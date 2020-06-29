using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotItemAmount : MonoBehaviour
{
    public void SetAmount(int amount)
    {
        TextMeshProUGUI textMesh = GetComponent<TextMeshProUGUI>();
        if (amount <= 0)
        {
            textMesh.enabled = false;
        }
        else
        {
            textMesh.enabled = true;
            textMesh.text = amount.ToString();
        }
    }
}

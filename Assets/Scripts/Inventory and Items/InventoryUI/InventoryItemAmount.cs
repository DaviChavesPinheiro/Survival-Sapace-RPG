using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemAmount : MonoBehaviour
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectSlot : MonoBehaviour, IPointerClickHandler
{
    Color originalColor;
    Color selectedColor = Color.white;

    private void Awake() {
        originalColor = GetComponent<Image>().color;
    }

    public void SetSelected(bool selected){
        if(selected){
            GetComponent<Image>().color = selectedColor;
        } else {
            GetComponent<Image>().color = originalColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.GetComponent<HootBarUI>().SetHootBarSlotSelected(transform.GetSiblingIndex());
    }
}

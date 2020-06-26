using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Canvas canvas;
    RectTransform rectTransform;

    private void Awake() {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        print(eventData.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

}

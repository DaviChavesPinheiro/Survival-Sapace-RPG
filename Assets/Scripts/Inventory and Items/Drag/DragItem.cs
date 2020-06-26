using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Canvas canvas;
    RectTransform rectTransform;

    Vector3 startPosition;
    public Transform originalParent;

    private void Awake() {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {   
        // if(eventData.pointerEnter)
        //     print("Click: " + eventData.pointerEnter.name);
        // else
        //     print("Click: " + eventData.pointerEnter);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.SetParent(originalParent, true);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Canvas canvas;
    RectTransform rectTransform;

    Vector2 startPosition;
    public Transform originalParent;

    private void Awake() {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
        print(startPosition);
        originalParent = transform.parent;

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent, true);
        rectTransform.anchoredPosition = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

}

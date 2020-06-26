using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Slot mySlot = gameObject.GetComponent<InventorySlotUI>().GetSlot();
        Slot receiveSlot = eventData.pointerDrag.GetComponent<DragItem>() ? eventData.pointerDrag.GetComponent<DragItem>().originalParent.GetComponent<InventorySlotUI>().GetSlot() : null;

        if(receiveSlot != null){
            gameObject.GetComponent<InventorySlotUI>().SetSlot(receiveSlot);
            eventData.pointerDrag.GetComponent<DragItem>().originalParent.GetComponent<InventorySlotUI>().SetSlot(mySlot);
            GetComponentInParent<InventoryUI>().UpdateInvetory();
        } else {
            print("Receive Slot Null");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] bool readOnly = false;
    public void OnDrop(PointerEventData eventData)
    {
        if(readOnly) return;
        Slot mySlot = gameObject.GetComponent<InventorySlotUI>().GetSlot();
        Slot receiveSlot = eventData.pointerDrag.GetComponent<DragItem>() ? eventData.pointerDrag.GetComponent<DragItem>().originalParent.GetComponent<InventorySlotUI>().GetSlot() : null;
        if(mySlot == receiveSlot) return;
        if(receiveSlot != null){
            //Fazer uma funcao statica no inventario (usar no proprio e aqui) que junta 2 slots e retorana um touple com o resultado
            if(Slot.hasEqualItemIDs(mySlot, receiveSlot) && mySlot.amount + receiveSlot.amount <= mySlot.maxAmount){
                mySlot.AddAmount(receiveSlot.amount);
                gameObject.GetComponent<InventorySlotUI>().SetSlot(mySlot);
                eventData.pointerDrag.GetComponent<DragItem>().originalParent.GetComponent<InventorySlotUI>().SetSlot(new Slot(null, 0));
            } else {
                gameObject.GetComponent<InventorySlotUI>().SetSlot(receiveSlot);
                eventData.pointerDrag.GetComponent<DragItem>().originalParent.GetComponent<InventorySlotUI>().SetSlot(mySlot);
            }
            
            InventoryUI mySlotInventoryUI = GetComponentInParent<InventoryUI>();
            InventoryUI receiveSlottInventoryUI = eventData.pointerDrag.GetComponent<DragItem>().originalParent.GetComponentInParent<InventoryUI>();
    
            if(mySlotInventoryUI == receiveSlottInventoryUI){
                mySlotInventoryUI.UpdateInvetory();
            } else {
                mySlotInventoryUI.UpdateInvetory(); 
                receiveSlottInventoryUI.UpdateInvetory();
            }
        } else {
            print("Receive Slot Null");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSlotToViewCraftRecipe : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponentInParent<CraftRecipesUIController>().OnSelectSlotToViewReceipe(GetComponent<InventorySlotUI>().GetSlot());
    }
}


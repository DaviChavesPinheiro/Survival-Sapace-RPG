using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUIControl : MonoBehaviour
{
    [SerializeField] PanelSlot rightSlot;
    [SerializeField] PanelSlot leftSlot;
    [SerializeField] InventoryUI playerInventoryUI;
    [SerializeField] InventoryUI chestInventoryUI;
    [SerializeField] InventoryUI craftTableInventoryUI;

    public void SetSubPanels(LeftSlot left, RightSlot right) {
        leftSlot.ActiveUI((int) left);
        rightSlot.ActiveUI((int) right);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        SetDefaultInventorySubPanels();
        ToggleUI();
    }

    public void SetDefaultInventorySubPanels()
    {
        SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.CraftUI);
    }

    public void ToggleUI()
    {
        rightSlot.gameObject.SetActive(!rightSlot.gameObject.activeSelf);
        leftSlot.gameObject.SetActive(!leftSlot.gameObject.activeSelf);
    }

    public void SetActiveUI(bool active)
    {
        rightSlot.gameObject.SetActive(active);
        leftSlot.gameObject.SetActive(active);
    }

    public void SetPlayerInventory(Inventory inventory){
        playerInventoryUI.SetInventory(inventory);
    }
    public void SetChestInventory(Inventory inventory){
        chestInventoryUI.SetInventory(inventory);
    }
    public void SetCraftTableInventory(Inventory inventory){
        craftTableInventoryUI.SetInventory(inventory);
    }

    public void SetCraftRecipesPanel(){
        leftSlot.ActiveUI((int) LeftSlot.CraftRecipesUI);
    }
}

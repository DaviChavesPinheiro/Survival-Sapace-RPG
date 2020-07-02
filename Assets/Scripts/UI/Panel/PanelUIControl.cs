using System;
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


    public event Action onPanelsChange;

    private void Start() {
        SetActiveUI(false);
    }

    public void SetSubPanels(LeftSlot left, RightSlot right) {
        leftSlot.ActiveUI((int) left);
        rightSlot.ActiveUI((int) right);
        if(onPanelsChange != null) onPanelsChange();
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

    public void SetCraftRecipesPanel(){
        leftSlot.ActiveUI((int) LeftSlot.CraftRecipesUI);
        if(onPanelsChange != null) onPanelsChange();
    }

    public void SetInventoryPanel(){
        leftSlot.ActiveUI((int) LeftSlot.PlayerInventoryUI);
        if(onPanelsChange != null) onPanelsChange();
    }

    public void SetDefaultInventorySubPanels()
    {
        SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.CraftUI);
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

    public Dictionary<string, int> GetActivesUIIndex(){
        return new Dictionary<string, int>(){{"leftSlot", leftSlot.activeChild}, {"rightSlot", rightSlot.activeChild}};
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class ChestController : MonoBehaviour, IInterectable
{
    private void Awake() {
        FindObjectOfType<SavingSystem>().onSaving += CaptureState;
        RestoreData();
    }

    public void CaptureState()
    {   
        SavingBlockState savingBlockState = FindObjectOfType(typeof(SavingBlockState)) as SavingBlockState;
        Dictionary<string, BlockData> blocksData = savingBlockState.blocksData;
        BlockData blockData = new BlockData();
        blockData.inventory = GetComponent<Inventory>().GetData();
        Vector2Int blockPostion = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        if(!blocksData.ContainsKey(blockPostion.ToString())){
            blocksData.Add(blockPostion.ToString(), blockData);
        } else {
            blocksData[blockPostion.ToString()] = blockData;
        }
    }

    private void RestoreData()
    {   
        Dictionary<string, BlockData> blocksData = (FindObjectOfType(typeof(SavingBlockState)) as SavingBlockState).blocksData;
        Vector2Int blockPostion = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
        if(!blocksData.ContainsKey(blockPostion.ToString())){
            return;
        }
        GetComponent<Inventory>().SetData(blocksData[blockPostion.ToString()].inventory);
    }

    public void OnInterect()
    {
        print("Im a cheast");
        PanelUIControl panelUIControl = FindObjectOfType<PanelUIControl>();
        panelUIControl.SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.ChestUI);
        panelUIControl.SetActiveUI(true);
        panelUIControl.SetChestInventory(GetComponent<Inventory>());
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class FurnaceController : MonoBehaviour, IInterectable
{
    private void Awake() {
        FindObjectOfType<SavingSystem>().onSaving += CaptureState;
        FindObjectOfType<Health>().onDie += DeleteData;
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
        if(!blocksData.ContainsKey(blockPostion.ToString())) return;
        GetComponent<Inventory>().SetData(blocksData[blockPostion.ToString()].inventory);
    }

    private void DeleteData()
    {
        string blockPostion = (new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y))).ToString();

        Dictionary<string, BlockData> blocksData = SavingBlockState.instance.blocksData;
        blocksData.Remove(blockPostion);
        FindObjectOfType<SavingSystem>().onSaving -= CaptureState;
    }

    public void OnInterect()
    {
        print("Im a CraftTable");
        PanelUIControl panelUIControl = FindObjectOfType<PanelUIControl>();
        panelUIControl.SetSubPanels(LeftSlot.PlayerInventoryUI, RightSlot.FurnaceUI);
        panelUIControl.SetActiveUI(true);
        panelUIControl.SetFurnaceInventory(GetComponent<Inventory>());
    }

}

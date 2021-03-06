﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class SavingBlockState : MonoBehaviour, ISaveable
{
    public static SavingBlockState instance;
    public Dictionary<string, BlockData> blocksData = new Dictionary<string, BlockData>();

	void Awake ()
	{
		instance = this;
	}

    public object CaptureState()
    {
        return blocksData;
    }

    public void RestoreState(object state)
    {
        blocksData = (Dictionary<string, BlockData>)state;
    }

    
}
[System.Serializable]
public struct BlockData
{
    public InventorySlotData[] inventory;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    #region Singleton

	public static GM instance;

	void Awake ()
	{
		instance = this;
		Crafts.Add(items.items[6].craftCode, items.items[6]);
		Crafts.Add(items.items[7].craftCode, items.items[7]);
	}

	#endregion

    public Items items;

	public Dictionary<string, Item> Crafts = new Dictionary<string, Item>();
}

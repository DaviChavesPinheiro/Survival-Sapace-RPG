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
		Crafts.Add("1|1|0|1|1|0|0|0|0", items.items[3]);
	}

	#endregion

    public Items items;

	public Dictionary<string, Item> Crafts = new Dictionary<string, Item>();
}

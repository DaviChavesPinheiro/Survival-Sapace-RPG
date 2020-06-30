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
		Crafts.Add("1|1|0|1|1|0|0|0|0", items.items[6]);
		Crafts.Add("1|1|1|1|8|1|1|1|1", items.items[7]);
	}

	#endregion

    public Items items;

	public Dictionary<string, Item> Crafts = new Dictionary<string, Item>();
}

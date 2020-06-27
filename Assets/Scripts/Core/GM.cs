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
	}

	#endregion

    public Items items;
}

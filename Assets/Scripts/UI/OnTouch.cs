using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouch : MonoBehaviour {

    [SerializeField] Buttons button;
    GameObject player;

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void OnTouchStationary(){
		switch (button) {
		case Buttons.accelerate:
			player.GetComponent<Movement>().Accelerate(true);
			break;
		}
	}

    enum Buttons{
        accelerate,
        fire,
        interact
    }
}

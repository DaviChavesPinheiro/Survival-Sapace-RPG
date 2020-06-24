using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class OnTouch : MonoBehaviour {

    [SerializeField] Buttons button;
    GameObject player;

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void OnTouchStationary(){
		switch (button) {
		case Buttons.accelerate:
			player.GetComponent<Movement>().isAccelerating = true;
            player.GetComponent<Movement>().Accelerate();
			break;
		case Buttons.fire:
			player.GetComponent<Shooter>().Shoot();
			break;
		}
	}
	public void OnTouchBegan(){
		switch (button) {
		case Buttons.save:
			GameObject.FindObjectOfType<SavingSystem>().Save("save");
			break;
		}
	}
	public void OnTouchEnded(){
		switch (button) {
		case Buttons.accelerate:
			player.GetComponent<Movement>().isAccelerating = false;
			break;
		}
	}
	public void OnTouchCanceled(){
		switch (button) {
		case Buttons.accelerate:
			player.GetComponent<Movement>().isAccelerating = false;
			break;
		}
	}

    enum Buttons{
        accelerate,
        fire,
        interact,
		save
    }
}

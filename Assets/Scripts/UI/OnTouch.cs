using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class OnTouch : MonoBehaviour {

    [SerializeField] Buttons button;
    GameObject player;
	Movement movement;
	Shooter shooter;

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		movement = player.GetComponent<Movement>();
		shooter = player.GetComponent<Shooter>();
	}

	public void OnTouchStationary(){
		switch (button) {
		case Buttons.accelerate:
			movement.isAccelerating = true;
            movement.Accelerate();
			break;
		case Buttons.fire:
			shooter.Shoot();
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
			movement.isAccelerating = false;
			break;
		}
	}
	public void OnTouchCanceled(){
		switch (button) {
		case Buttons.accelerate:
			movement.isAccelerating = false;
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

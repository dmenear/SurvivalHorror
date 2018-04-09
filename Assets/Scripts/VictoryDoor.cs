﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDoor : MonoBehaviour {

	public GameObject Door, TextBox;
	public AudioClip DoorCreak, LockedSound;
	public float Distance;
	public GameStateManager StateManager;
	public bool DoorLocked = true, DisplayingText = false;
	
	void Update () {
		Distance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver (){
		if (Distance <= 2.5) {
			if (Input.GetButtonDown ("Action")) {
				if (DoorLocked) {
					if (!DisplayingText) {
						Door.GetComponent<AudioSource> ().PlayOneShot (LockedSound, 0.7f);
						StartCoroutine (DisplayDoorLocked ());
					}
				} else {
					Door.GetComponent<AudioSource> ().PlayOneShot (DoorCreak, 0.7f);
					StateManager.Victory ();
				}
			}
		}
	}

	IEnumerator DisplayDoorLocked(){
		TextBox.GetComponent<Text> ().text = "This door is locked.";
		DisplayingText = true;
		yield return new WaitForSecondsRealtime (3.0f);
		DisplayingText = false;
		TextBox.GetComponent<Text> ().text = "";
	}
}

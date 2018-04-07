﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestChamberTrigger : MonoBehaviour {

	public static bool isActivated;
	public GameObject Player, Door, DoorTrigger;

	// Use this for initialization
	void Start () {
		isActivated = false;
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject == Player.gameObject) {
			isActivated = true;
			if (DoorTrigger.GetComponent<DoorCellOpen> ().SlamBehind) {
				DoorTrigger.GetComponent<DoorCellOpen> ().SlamBehind = false;
				DoorTrigger.GetComponent<DoorCellOpen> ().SlamDoorBehind ();
				DoorTrigger.GetComponent<DoorCellOpen> ().DoorLocked = true;
			}
		}
	}
}

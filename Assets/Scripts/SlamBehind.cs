﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamBehind : MonoBehaviour {

	public GameObject door, player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == player.gameObject && door.GetComponent<DoorCellOpen>().slamBehind) {
			door.GetComponent<DoorCellOpen> ().slamBehind = false;
			door.GetComponent<DoorCellOpen> ().SlamBehind ();
			door.GetComponent<DoorCellOpen> ().DoorLocked = true;
		}
	}
}

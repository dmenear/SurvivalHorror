using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryDoor : MonoBehaviour {

	public GameObject Door;
	public AudioClip DoorCreak;
	public float Distance;
	public GameStateManager StateManager;
	
	void Update () {
		Distance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver (){
		if (Distance <= 2.5) {
			if (Input.GetButtonDown ("Action")) {
				Door.GetComponent<AudioSource> ().PlayOneShot (DoorCreak, 0.7f);
				StateManager.Victory ();
			}
		}
	}
}

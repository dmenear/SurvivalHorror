using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpiderZone2 : MonoBehaviour {

	public static bool InZone;
	public GameObject Player;

	void Start () {
		InZone = false;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == Player.gameObject) {
			InZone = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == Player.gameObject) {
			InZone = false;
		}
	}
}

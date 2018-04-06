using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpiderZone1 : MonoBehaviour {

	public static bool InZone = false;
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

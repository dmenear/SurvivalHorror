using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMiniSpiderZone : MonoBehaviour {

	public GameObject Player;
	public static bool InZone;
	public bool visibleInZone;

	void Start () {
		InZone = false;
		visibleInZone = false;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject == Player.gameObject) {
			InZone = true;
			visibleInZone = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject == Player.gameObject) {
			InZone = false;
			visibleInZone = false;
		}
	}
}

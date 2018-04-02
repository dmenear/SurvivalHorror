using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackZone1 : MonoBehaviour {

	public static bool inAttackZone = false;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == player.gameObject) {
			inAttackZone = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject == player.gameObject) {
			inAttackZone = false;
		}
	}
}

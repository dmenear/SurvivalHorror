using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSafetyZone1 : MonoBehaviour {

	public GameObject player, monster;
	public static bool inSafetyZone1 = false;
	List<GameObject> inZone;

	void Start(){
		inZone = new List<GameObject> ();
	}

	void Update(){
		if (inZone.Contains (player) && !inZone.Contains (monster)) {
			inSafetyZone1 = true;
		} else {
			inSafetyZone1 = false;
		}
	}

	void OnTriggerEnter(Collider other){
		if (!inZone.Contains (other.gameObject)) {
			inZone.Add (other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (inZone.Contains (other.gameObject)) {
			inZone.Remove (other.gameObject);
		}
	}

}

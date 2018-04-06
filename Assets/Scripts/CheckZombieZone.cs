using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckZombieZone : MonoBehaviour {

	public GameObject Player, Monster;
	public static bool SafelyInZone = false;
	List<GameObject> inZone;

	void Start(){
		inZone = new List<GameObject> ();
	}

	void Update(){
		if (!inZone.Contains (Player) && inZone.Contains (Monster)) {
			SafelyInZone = true;
		} else {
			SafelyInZone = false;
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

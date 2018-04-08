using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestExitTrigger : MonoBehaviour {

	public GameObject Player, Door, DoorTrigger;
	public GameObject[] Monsters;
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject == Player.gameObject) {
			if (DoorTrigger.GetComponent<DoorCellOpen> ().SlamBehind) {
				DoorTrigger.GetComponent<DoorCellOpen> ().SlamBehind = false;
				DoorTrigger.GetComponent<DoorCellOpen> ().SlamDoorBehind ();
				DoorTrigger.GetComponent<DoorCellOpen> ().DoorLocked = true;
				StartCoroutine (killMonsters ());
			}
		}
	}

	IEnumerator killMonsters(){
		yield return new WaitForSecondsRealtime (0.5f);
		foreach (GameObject monster in Monsters) {
			monster.SetActive (false);
		}
	}
}

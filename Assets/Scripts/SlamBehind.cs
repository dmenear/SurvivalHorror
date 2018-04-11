using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamBehind : MonoBehaviour {

	public GameObject door, player, monster;
	public bool spider;
	public MusicController music;

	void OnTriggerEnter(Collider other){
		if (other.gameObject == player.gameObject && door.GetComponent<DoorCellOpen>().SlamBehind) {
			door.GetComponent<DoorCellOpen> ().SlamBehind = false;
			door.GetComponent<DoorCellOpen> ().SlamDoorBehind ();
			door.GetComponent<DoorCellOpen> ().DoorLocked = true;
			if (spider) {
				StartCoroutine (playSpiderMusic ());
			}
			if (door.GetComponent<DoorCellOpen>().Complete) {
				StartCoroutine (door.GetComponent<DoorCellOpen>().endMusic ());
				door.GetComponent<DoorCellOpen> ().DoorLocked = true;
			}
			StartCoroutine (disableZone ());
		}
	}

	IEnumerator playSpiderMusic(){
		yield return new WaitForSeconds (1.0f);
		monster.GetComponent<Monster2Controller> ().Roar ();
		yield return new WaitForSeconds (1.5f);
		music.changeToSpider ();
	}

	IEnumerator disableZone(){
		yield return new WaitForSeconds (5.0f);
		this.gameObject.SetActive (false);
	}
}

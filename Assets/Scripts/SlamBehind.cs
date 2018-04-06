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
		}
	}

	IEnumerator playSpiderMusic(){
		yield return new WaitForSecondsRealtime (1.0f);
		monster.GetComponent<Monster2Controller> ().Roar ();
		yield return new WaitForSecondsRealtime (1.5f);
		music.changeToSpider ();
	}
}

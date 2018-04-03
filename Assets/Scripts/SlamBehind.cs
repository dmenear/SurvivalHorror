using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamBehind : MonoBehaviour {

	public GameObject door, player, monster;
	public bool spider;
	public MusicController music;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == player.gameObject && door.GetComponent<DoorCellOpen>().slamBehind) {
			door.GetComponent<DoorCellOpen> ().slamBehind = false;
			door.GetComponent<DoorCellOpen> ().SlamBehind ();
			door.GetComponent<DoorCellOpen> ().DoorLocked = true;
			if (spider) {
				StartCoroutine (playSpiderMusic ());
			}
		}
	}

	IEnumerator playSpiderMusic(){
		yield return new WaitForSecondsRealtime (1.0f);
		monster.GetComponent<Monster2Controller> ().roar ();
		yield return new WaitForSecondsRealtime (1.5f);
		music.changeToSpider ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastDoor : MonoBehaviour {

	public GameObject Beast;
	public MusicController music;
	bool created;

	void Start(){
		created = false;
	}

	void Update () {
		if (GetComponent<DoorCellOpen> ().DoorOpen && !created) {
			created = true;
			StartCoroutine (createBeast ());
		}
	}

	IEnumerator createBeast(){
		yield return new WaitForSecondsRealtime (28.0f);
		Beast.SetActive (true);
		music.audio.Stop ();
		yield return new WaitForSecondsRealtime (3.5f);
		music.changeToMaze ();
	}
}

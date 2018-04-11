using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint2Checker : MonoBehaviour {

	public GameObject Keys;
	public CheckpointHandler CH;
	bool activated;
	public bool doorFinished;

	void Start(){
		activated = false;
		doorFinished = false;
	}

	// Update is called once per frame
	void Update () {
		if (!activated && Keys.GetComponent<PickUpKey2>().pickedUp && !GetComponent<DoorCellOpen>().DoorOpen && doorFinished && PlayerPrefs.GetInt("Checkpoint2Reached") != 1) {
			activated = true;
			StartCoroutine (setCheckpoint());
		}
	}

	IEnumerator setCheckpoint(){
		yield return new WaitForSeconds (1.2f);
		CH.checkPoint2Reached = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1Checker : MonoBehaviour {

	public GameObject Zombie;
	public CheckpointHandler CH;
	bool activated;

	void Start(){
		activated = false;
	}

	// Update is called once per frame
	void Update () {
		if (GetComponent<DoorCellOpen> ().DoorOpen && !activated && !Zombie.activeSelf && PlayerPrefs.GetInt("Checkpoint1Reached") != 1) {
			CH.checkPoint1Reached = true;
			activated = true;
		}
	}
}

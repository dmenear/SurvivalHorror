using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint3Checker : MonoBehaviour {

	public CheckpointHandler CH;
	bool activated;
	public bool spiderDead;

	void Start(){
		activated = false;
		spiderDead = false;
	}

	// Update is called once per frame
	void Update () {
		if (!activated && spiderDead && PlayerPrefs.GetInt("Checkpoint3Reached") != 1) {
			activated = true;
			StartCoroutine (setCheckpoint());
		}
	}

	IEnumerator setCheckpoint(){
		yield return new WaitForSeconds (4.0f);
		CH.checkPoint3Reached = true;
	}
}

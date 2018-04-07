using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4Controller : MonoBehaviour {

	public GameObject Door;
	public AudioClip shout;
	public GameObject[] zombies;
	bool activated, waiting;

	// Use this for initialization
	void Start () {
		activated = false;
		waiting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (WestChamberTrigger.isActivated && !activated) {
			waiting = true;
			activated = true;
			StartCoroutine (activateZombies());
		}
		if (waiting) { //
			GetComponent<Animator> ().SetBool ("isShouting", true);
			StartCoroutine (shoutSound ());
			StartCoroutine (waitForNextShout ());
			waiting = false;
		}


	}

	IEnumerator shoutSound(){
		yield return new WaitForSecondsRealtime (0.7f);
		GetComponent<Animator> ().SetBool ("isShouting", false);
		GetComponent<AudioSource> ().PlayOneShot (shout, 1.3f);
		foreach (GameObject z in zombies) {
			z.GetComponent<FinalZombieController>().Boost ();
		}
	}

	IEnumerator waitForNextShout(){
		yield return new WaitForSecondsRealtime (Random.Range (15.0f, 35.0f));
		waiting = true;
	}

	IEnumerator activateZombies(){
		yield return new WaitForSecondsRealtime (3.7f);
		foreach (GameObject z in zombies) {
			z.GetComponent<FinalZombieController> ().Activated = true;
		}
	}
}

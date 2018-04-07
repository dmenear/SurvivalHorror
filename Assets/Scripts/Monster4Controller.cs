using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4Controller : MonoBehaviour {

	public GameObject Door, SecondKey, Player;
	public AudioClip shout;
	public GameObject[] zombies;
	bool activated, waiting, jumpingDown, finishJump, onFloor, finishShouting;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		activated = false;
		waiting = false;
		jumpingDown = false;
		finishJump = false;
		onFloor = false;
		finishShouting = false;
	}
	
	// Update is called once per frame
	void Update () {
		direction = Player.transform.position - this.transform.position;
		direction.y = 0;
		if (SecondKey.GetComponent<PickUpKey3> ().pickedUp) {
			SecondKey.GetComponent<PickUpKey3> ().pickedUp = false;
			StartCoroutine (waitBeforeJumping ());
		}
		if (WestChamberTrigger.isActivated && !activated) {
			waiting = true;
			activated = true;
			StartCoroutine (activateZombies());
		}
		if (jumpingDown) {
			this.transform.Translate (0, (finishJump ? -4.0f : -1.5f)  * Time.deltaTime, (finishJump ? 2.0f : 6.0f) * Time.deltaTime);
		} else if (waiting) { //
			GetComponent<Animator> ().SetBool ("isShouting", true);
			StartCoroutine (shoutSound ());
			StartCoroutine (waitForNextShout ());
			waiting = false;
		}
		if (onFloor) {
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.02f);
			this.transform.Translate (0, 0, 7.5f * Time.deltaTime);
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
		if(!onFloor && !jumpingDown) waiting = true;
	}

	IEnumerator activateZombies(){
		yield return new WaitForSecondsRealtime (3.7f);
		foreach (GameObject z in zombies) {
			z.GetComponent<FinalZombieController> ().Activated = true;
		}
	}

	IEnumerator waitBeforeJumping(){
		waiting = false;
		yield return new WaitForSecondsRealtime (1.5f);
		GetComponent<Animator> ().SetBool ("isShouting", false);
		GetComponent<Animator> ().SetBool ("isJumping", true);
		yield return new WaitForSecondsRealtime (0.5f);
		jumpingDown = true;
		yield return new WaitForSecondsRealtime (0.8f);
		finishJump = true;
		yield return new WaitForSecondsRealtime (0.2f);
		jumpingDown = false;
		yield return new WaitForSecondsRealtime (0.2f);
		GetComponent<Animator> ().SetBool ("isJumping", false);
		GetComponent<Animator> ().SetBool ("isChasing", true);
		onFloor = true;
		StartCoroutine (takeABreak ());
	}

	IEnumerator takeABreak(){
		yield return new WaitForSecondsRealtime (12.0f);
		onFloor = false;
		GetComponent<Animator> ().SetBool ("isChasing", false);
		GetComponent<Animator> ().SetBool ("isShouting", true);
		StartCoroutine (shoutSound ());
		yield return new WaitForSecondsRealtime (5.0f);
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		GetComponent<Animator> ().SetBool ("isChasing", true);
		GetComponent<Animator> ().SetBool ("isShouting", false);
		onFloor = true;
		StartCoroutine (takeABreak ());
	}
}

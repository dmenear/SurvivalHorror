using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4Controller : MonoBehaviour {

	public GameObject Door, SecondKey, Player, ThirdKey, FallingBones, CorrectPillar;
	public AudioClip shout;
	public GameObject[] zombies;
	bool activated, waiting, jumpingDown, finishJump, itemsDropped, onFloor, walkBackwards;
	bool finishShouting, secondKeyObtained, hitPillar, pillarReactionStarted;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		activated = false;
		waiting = false;
		jumpingDown = false;
		finishJump = false;
		onFloor = false;
		finishShouting = false;
		secondKeyObtained = false;
		hitPillar = false;
		pillarReactionStarted = false;
		walkBackwards = false;
		itemsDropped = false;
	}
	
	// Update is called once per frame
	void Update () {
		direction = Player.transform.position - this.transform.position;
		direction.y = 0;
		if (SecondKey.activeSelf) {
			if (!secondKeyObtained) {
				if (SecondKey.GetComponent<PickUpKey1> ().pickedUp) {
					secondKeyObtained = true;
					StartCoroutine (waitBeforeJumping ());
				}
			}
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
		if (hitPillar && !pillarReactionStarted) {
			pillarReactionStarted = true;
			StartCoroutine (pillarSequence ());
		} else if (onFloor) {
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.02f);
			this.transform.Translate (0, 0, 7.5f * Time.deltaTime);
		} else if (walkBackwards) {
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.02f);
			this.transform.Translate (0, 0, -1.5f * Time.deltaTime);
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

	void OnCollisionEnter(Collision other){
		if(other.gameObject.layer == LayerMask.NameToLayer("CollisionPillars")){
			hitPillar = true;
			if (other.gameObject == CorrectPillar && !itemsDropped) {
				dropItems ();
			}
		}
	}

	IEnumerator pillarSequence(){
		GetComponent<Animator> ().SetBool ("isChasing", false);
		GetComponent<Animator> ().SetBool ("hitPillar", true);
		onFloor = false;
		this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		yield return new WaitForSecondsRealtime (1.2f);
		walkBackwards = true;
		yield return new WaitForSecondsRealtime (1.5f);
		walkBackwards = false;
		GetComponent<Animator> ().SetBool ("isChasing", true);
		GetComponent<Animator> ().SetBool ("hitPillar", false);
		hitPillar = false;
		pillarReactionStarted = false;
		onFloor = true;
	}

	void dropItems(){
		foreach (Transform bone in FallingBones.transform) {
			bone.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		}
		ThirdKey.GetComponent<Rigidbody> ().isKinematic = false;
		itemsDropped = true;
	}
}

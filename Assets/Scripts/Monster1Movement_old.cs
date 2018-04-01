using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1Movement_old : MonoBehaviour {

	public Transform player;
	Animator anim;
	string state = "idle";
	int startDistance = 20, stopDistance = 30;
	public GameObject[] waypoints;
	int currentWP = 0;
	float rotSpeed = 0.7f;
	float followSpeed = 2.5f, patrolSpeed = 1.0f;
	float accuracyWP = 2.0f;
	public AudioClip roar;
	AudioSource audio;
	bool roared = false;
	public GameObject doorTrigger;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		if (Vector3.Distance (player.position, this.transform.position) < startDistance && doorTrigger.GetComponent<DoorCellOpen>().StateChanging) {
			state = "follow";
		}
		if (state == "patrol" && waypoints.Length > 0) {
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isPatrolling", true);
			if (Vector3.Distance (waypoints [currentWP].transform.position, transform.position) < accuracyWP) {
				currentWP++;
				if (currentWP >= waypoints.Length) {
					currentWP = 0;
				}
			}
			direction = waypoints [currentWP].transform.position - transform.position;
			this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
			this.transform.Translate (0, 0, patrolSpeed * Time.deltaTime);
		}
		if (Vector3.Distance (player.position, this.transform.position) < stopDistance && state == "follow") {
			if (!roared) {
				audio.PlayOneShot (roar, 0.4f);
				roared = true;
			}
			anim.SetBool ("isPatrolling", false);
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", true);
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

			if (direction.magnitude > 1.5f) {
				this.transform.Translate (0, 0, followSpeed * Time.deltaTime);
				anim.SetBool ("isWalking", true);
				anim.SetBool ("isAttacking", false);
			} else {
				anim.SetBool ("isWalking", false);
				anim.SetBool ("isAttacking", true);
			}
		}
		else {
			state = "idle";
			anim.SetBool ("isPatrolling", false);
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
			anim.SetBool ("isAttacking", false);
		}
	}
}

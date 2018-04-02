using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Controller : MonoBehaviour {

	Animator anim;
	public GameObject player, flashlight, door, jumpScare;
	string state;
	public Vector3 startPosition;
	bool killed = false;
	public GameStateManager stateManager;
	AudioSource audioJumpScare;
	public AudioClip jumpScareSound;
	public MusicController music;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		state = "idle";
		startPosition = transform.position;
		audioJumpScare = stateManager.GetComponent<AudioSource> ();
	}

	//Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		direction.y = 0;
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		if (flashlight.activeSelf && !anim.GetBool ("isChasing") && (CheckAttackZone.inAttackZone || CheckAttackZone1.inAttackZone)) {
			anim.SetBool ("isChasing", true);
			state = "chase";
		} else if ((!flashlight.activeSelf && anim.GetBool ("isChasing")) || (!CheckAttackZone.inAttackZone && !CheckAttackZone1.inAttackZone)) {
			anim.SetBool ("isChasing", false);
			state = "idle";
		}
		if (CheckAttackZone1.inAttackZone) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, door.transform.position.z);
		}
		if (state == "chase") {
			this.transform.Translate (0, 0, 10f * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject == player.gameObject && !killed){
			killed = true;
			jumpScare.SetActive (true);
			music.audio.Stop ();
			audioJumpScare.PlayOneShot (jumpScareSound, 1.0f);
			stateManager.EndGame ();
		}
	}
}

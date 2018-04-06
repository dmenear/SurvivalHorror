using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Controller : MonoBehaviour {

	public GameObject Player, Flashlight, Door, JumpScare;

	public GameStateManager StateManager;
	public AudioClip JumpScareAudio, SpiderRoar, SpiderUpClose;
	public MusicController Music;
	public Vector3 StartPosition;

	Animator anim;
	string state;
	bool killed = false, upClosePlayed = false;
	AudioSource audioJumpScare, audio;
	float mag;


	// Use this for initialization
	void Start () {
		killed = false;
		upClosePlayed = false;
		anim = GetComponent<Animator> ();
		state = "idle";
		StartPosition = transform.position;
		audioJumpScare = StateManager.GetComponent<AudioSource> ();
		audio = GetComponent<AudioSource> ();
	}

	//Update is called once per frame
	void Update () {
		Vector3 direction = Player.transform.position - this.transform.position;
		direction.y = 0;
		mag = direction.magnitude;
		if (direction.magnitude <= 9 && !upClosePlayed) {
			upClosePlayed = true;
			StartCoroutine (upCloseSound());
		}
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		if (Flashlight.activeSelf && !anim.GetBool ("isChasing") && (CheckSpiderZone1.InZone || CheckSpiderZone2.InZone)) {
			anim.SetBool ("isChasing", true);
			state = "chase";
		} else if ((!Flashlight.activeSelf && anim.GetBool ("isChasing")) || (!CheckSpiderZone1.InZone && !CheckSpiderZone2.InZone)) {
			anim.SetBool ("isChasing", false);
			state = "idle";
		}
		if (CheckSpiderZone2.InZone) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, Door.transform.position.z);
		}
		if (state == "chase") {
			this.transform.Translate (0, 0, 9f * Time.deltaTime);
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject == Player.gameObject && !killed) {
			killed = true;
			JumpScare.SetActive (true);
			Music.audio.Stop ();
			audioJumpScare.PlayOneShot (JumpScareAudio, 1.5f);
			StateManager.EndGame ();
		}
	}

	public void Roar(){
		audio.PlayOneShot (SpiderRoar, 1.0f);
	}

	IEnumerator upCloseSound(){
		audio.PlayOneShot (SpiderUpClose, 0.8f);
		yield return new WaitForSecondsRealtime (10.0f);
	}
}

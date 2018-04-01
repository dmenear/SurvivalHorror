using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1Movement : MonoBehaviour {

	public Transform player;
	Animator anim;
	string state = "idle";
	float followSpeed = 3.2f;
	public AudioClip roar, jumpScareSound, roar0, roar1, roar2;
	AudioSource audio, audioJumpScare;
	bool roared = false, killed = false;
	public GameObject doorTrigger, jumpScare;
	public MusicController music;
	public GameStateManager stateManager;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		audioJumpScare = stateManager.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.position - this.transform.position;
		direction.y = 0;
		if (doorTrigger.GetComponent<DoorCellOpen> ().StateChanging) {
			state = "follow";
		} else if(!doorTrigger.GetComponent<DoorCellOpen>().DoorOpen && CheckSafetyZone1.inSafetyZone1) {
			doorTrigger.GetComponent<DoorCellOpen> ().enabled = false;
			music.changeToAmbient ();
			Destroy (gameObject);
		}
		if (state == "follow") {
			if (!roared) {
				audio.PlayOneShot (roar, 0.6f);
				music.changeToChase ();
				roared = true;
				StartCoroutine (PeriodicSound());
			}
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", true);
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
			this.transform.Translate (0, 0, followSpeed * Time.deltaTime);
		}
		else {
			state = "idle";
			anim.SetBool ("isIdle", true);
			anim.SetBool ("isWalking", false);
		}
	}

	void OnCollisionEnter(Collision other){
		if(other.gameObject == player.gameObject && !killed){
			killed = true;
			player.Find ("Flashlight").gameObject.SetActive (false);
			jumpScare.SetActive (true);
			music.audio.Stop ();
			audioJumpScare.PlayOneShot (jumpScareSound, 1.0f);
			stateManager.EndGame ();
		}
	}

	IEnumerator PeriodicSound(){
		yield return new WaitForSeconds (4f);
		int sound = Random.Range (0, 3);
		switch (sound) {
		case 0:
			audio.PlayOneShot (roar0, 0.6f);
			break;
		case 1:
			audio.PlayOneShot (roar1, 0.6f);
			break;
		case 2:
			audio.PlayOneShot (roar2, 0.6f);
			break;
		}
		StartCoroutine (PeriodicSound ());
	}
		
}

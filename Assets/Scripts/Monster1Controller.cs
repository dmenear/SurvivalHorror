using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1Controller : MonoBehaviour {

	public Transform Player;
	public AudioClip JumpScareAudio, Roar1, Roar2, Roar3, Roar4;
	public GameObject DoorTrigger, JumpScare;
	public MusicController Music;
	public GameStateManager StateManager;

	Animator anim;
	string state = "idle";
	float followSpeed = 3.2f;
	AudioSource audio, audioJumpScare;
	bool roared, killed;

	void Start () {
		roared = false;
		killed = false;
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		audioJumpScare = StateManager.GetComponent<AudioSource> ();
	}

	void Update () {
		Vector3 direction = Player.position - this.transform.position;
		direction.y = 0;
		if (DoorTrigger.GetComponent<DoorCellOpen> ().StateChanging) {
			state = "follow";
		} else if(!DoorTrigger.GetComponent<DoorCellOpen>().DoorOpen && CheckZombieZone.SafelyInZone) {
			DoorTrigger.GetComponent<DoorCellOpen> ().DoorLocked = true;
			Music.changeToAmbient ();
			gameObject.SetActive (false);
		}
		if (state == "follow") {
			if (!roared) {
				audio.PlayOneShot (Roar1, 0.6f);
				Music.changeToChase ();
				roared = true;
				StartCoroutine (periodicSound());
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
		if(other.gameObject == Player.gameObject && !killed){
			killed = true;
			JumpScare.SetActive (true);
			Music.audio.Stop ();
			audioJumpScare.PlayOneShot (JumpScareAudio, 1.5f);
			StateManager.EndGame ();
		}
	}

	IEnumerator periodicSound(){
		yield return new WaitForSecondsRealtime (4f);
		int sound = Random.Range (0, 3);
		switch (sound) {
		case 0:
			audio.PlayOneShot (Roar2, 0.6f);
			break;
		case 1:
			audio.PlayOneShot (Roar3, 0.6f);
			break;
		case 2:
			audio.PlayOneShot (Roar4, 0.6f);
			break;
		}
	}
		
}

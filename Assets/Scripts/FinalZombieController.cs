using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZombieController : MonoBehaviour {

	public Transform Player;
	public AudioClip JumpScareAudio, Roar1, Roar2, Roar3, Roar4;
	public GameObject DoorTrigger, JumpScare;
	public MusicController Music;
	public GameStateManager StateManager;
	public bool FastZombie, Activated;
	public float rotSpeed = 3.0f;

	Animator anim;
	float followSpeed;
	AudioSource audio, audioJumpScare;
	bool roared, killed;

	void Start () {
		Activated = false;
		roared = false;
		killed = false;
		anim = GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		audioJumpScare = StateManager.GetComponent<AudioSource> ();
		if (FastZombie) {
			followSpeed = 3.9f;
			anim.speed = 1.1f;
		} else {
			followSpeed = 3.0f;
		}
	}

	void Update () {
		Vector3 direction = Player.position - this.transform.position;
		direction.y = 0;
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), rotSpeed * Time.deltaTime);
		if (Activated) {
			if (!roared) {
				if (FastZombie) {
					Music.changeToFinalBoss ();
				}
				roared = true;
				StartCoroutine (periodicSound());
			}
			anim.SetBool ("isIdle", false);
			anim.SetBool ("isWalking", true);
			this.transform.Translate (0, 0, followSpeed * Time.deltaTime);
		}
		else {
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
		yield return new WaitForSeconds (Random.Range(4.0f, 8.0f));
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
		StartCoroutine (periodicSound ());
	}

	public void Boost(){
		followSpeed += 0.1f;
		anim.speed += 0.02f;
		StartCoroutine (delayRoar ());
	}

	IEnumerator delayRoar(){
		yield return new WaitForSeconds (2.7f);
		audio.PlayOneShot (Roar1, 0.6f);
	}
		
}

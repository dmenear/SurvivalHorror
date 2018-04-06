using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScare : MonoBehaviour {

	public GameObject Player, Flashlight, Skeleton;
	public AudioClip Flicker;

	bool triggered;

	void Start(){
		triggered = false;
	}

	void OnTriggerEnter(Collider other){
		if (!triggered && other.gameObject == Player.gameObject) {
			triggered = true;
			StartCoroutine (playScare ());
		}
	}

	IEnumerator playScare(){
		Flashlight.GetComponent<Animation> ().Play ("FlashlightFlicker");
		Player.GetComponents<AudioSource>()[1].PlayOneShot (Flicker, 0.7f);
		yield return new WaitForSecondsRealtime (0.3f);
		Flashlight.GetComponent<Light> ().enabled = false;
		Skeleton.SetActive (false);
		yield return new WaitForSecondsRealtime (5.0f);
		Flashlight.SetActive (true);
		Flashlight.GetComponent<Light> ().enabled = true;
		Player.GetComponents<AudioSource>()[1].PlayOneShot (Flicker, 0.7f);
	}
}

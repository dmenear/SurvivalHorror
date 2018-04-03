using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScare : MonoBehaviour {

	public GameObject player, flashlight, skeleton;
	public AudioClip flicker;
	bool triggered;

	void Start(){
		triggered = false;
	}

	void OnTriggerEnter(Collider other){
		if (!triggered && other.gameObject == player.gameObject) {
			triggered = true;
			StartCoroutine (playScare ());
		}
	}

	IEnumerator playScare(){
		flashlight.GetComponent<Animation> ().Play ("FlashlightFlicker");
		player.GetComponents<AudioSource>()[1].PlayOneShot (flicker, 0.7f);
		yield return new WaitForSecondsRealtime (0.3f);
		flashlight.GetComponent<Light> ().enabled = false;
		skeleton.SetActive (false);
		yield return new WaitForSecondsRealtime (5.0f);
		flashlight.SetActive (true);
		flashlight.GetComponent<Light> ().enabled = true;
		player.GetComponents<AudioSource>()[1].PlayOneShot (flicker, 0.7f);
	}
}

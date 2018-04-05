using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerControls : MonoBehaviour {

	bool isActive;
	AudioSource audio;

	void Start(){
		isActive = false;
		audio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Shoot") && isActive) {
			GetComponent<ParticleSystem> ().Stop ();
			isActive = false;
			audio.Stop ();
			audio.enabled = false;
		} else if (Input.GetButtonDown ("Shoot") && !isActive) {
			GetComponent<ParticleSystem> ().Play ();
			isActive = true;
			audio.enabled = true;
			audio.Play ();
		}
	}

}

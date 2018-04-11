﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class FlamethrowerControls : MonoBehaviour {

	public Transform Environment;
	public ControllerFixes cFixes;
	public GameObject FlashLight, Fire, Player, TextBox;
	public GameObject[] Pieces;
	bool isActive, assembled;
	AudioSource audio;

	void Start(){
		isActive = false;
		assembled = true;
		audio = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (assembled) {
			if ((Input.GetButtonUp ("Shoot") || (Input.GetAxis("ControllerShoot") == 0 && cFixes.controllerUsed)) && isActive) {
				Fire.GetComponent<ParticleSystem> ().Stop ();
				isActive = false;
				audio.Stop ();
				audio.enabled = false;
			} else if ((Input.GetButtonDown ("Shoot") || (Input.GetAxis("ControllerShoot") > 0 && cFixes.controllerUsed)) && !isActive) {
				Fire.GetComponent<ParticleSystem> ().Play ();
				isActive = true;
				audio.enabled = true;
				audio.Play ();
			}
		}
	}

	public IEnumerator Disassemble(){
		if (assembled && this.gameObject.activeSelf) {
			Fire.GetComponent<ParticleSystem> ().Stop ();
			isActive = false;
			audio.Stop ();
			audio.enabled = false;
			assembled = false;
			foreach (GameObject child in Pieces) {
				child.transform.SetParent (Environment);
				child.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
				if (child.gameObject.GetComponent<MeshCollider> () != null) {
					child.gameObject.GetComponent<MeshCollider> ().enabled = true;
				} else if (child.gameObject.GetComponent<SphereCollider> () != null) {
					child.gameObject.GetComponent<SphereCollider> ().enabled = true;
				} else {
					child.gameObject.GetComponent<BoxCollider> ().enabled = true;
				}
			}
			FlashLight.SetActive (false);
			Player.GetComponent<FirstPersonController> ().flashlightEnabled = true;
			Player.GetComponent<CharacterController> ().radius = 0.5f;
			Player.GetComponent<CharacterController> ().center = new Vector3(0.0f, 0.0f, 0.0f);
			TextBox.GetComponent<Text> ().text = "Your flamethrower fell apart.";
			yield return new WaitForSeconds (4.0f);
			TextBox.GetComponent<Text> ().text = "";
		}
	}

}

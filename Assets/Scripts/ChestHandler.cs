using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestHandler : MonoBehaviour {

	public GameObject Player, Chest, TextBox;
	public AudioClip Creak, LockedSound;
	public bool chestOpen, mouseOver, chestLocked, DisplayingText;
	public float Distance;

	void Start(){
		chestOpen = false;
		mouseOver = false;
	}

	void Update(){
		Vector3 direction = Player.transform.position - this.transform.position;
		Distance = direction.magnitude;
		if (Distance <= 2.5f && !chestOpen && mouseOver) {
			if (Input.GetButtonDown ("Action")) {
				if (!chestLocked) {
					Chest.GetComponent<AudioSource> ().PlayOneShot (Creak, 0.7f);
					Chest.GetComponent<Animator> ().SetBool ("isOpen", true);
					chestOpen = true;
				} else {
					if (!DisplayingText) {
						Chest.GetComponent<AudioSource> ().PlayOneShot (LockedSound, 0.7f);
						StartCoroutine (DisplayChestLocked ());
					}
				}
			}
		}
	}

	IEnumerator DisplayChestLocked(){
		TextBox.GetComponent<Text> ().text = "This chest is locked.";
		DisplayingText = true;
		yield return new WaitForSecondsRealtime (3.0f);
		DisplayingText = false;
		TextBox.GetComponent<Text> ().text = "";
	}

	void OnMouseEnter () {
		mouseOver = true;
	}

	void OnMouseExit(){
		mouseOver = false;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestHandler : MonoBehaviour {

	public GameObject Player, Chest, TextBox, objectInChest;
	public AudioClip Creak, LockedSound;
	public InteractiveHandler IH;
	public bool chestOpen, chestLocked, DisplayingText, timedUnlock;

	void Start(){
		chestOpen = false;
		timedUnlock = false;
	}

	void Update(){
		if (IH.Interactable.Contains(this.gameObject) && !chestOpen) {
			if (Input.GetButtonDown ("Action")) {
				if (!chestLocked) {
					Chest.GetComponent<AudioSource> ().PlayOneShot (Creak, 0.7f);
					Chest.GetComponent<Animator> ().SetBool ("isOpen", true);
					chestOpen = true;
					if (objectInChest != null && !objectInChest.GetComponent<ObjectInChest>().ChestOpen) {
						StartCoroutine (delayPickup());
					}
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
		yield return new WaitForSeconds (3.0f);
		DisplayingText = false;
		TextBox.GetComponent<Text> ().text = "";
	}

	IEnumerator delayPickup(){
		if (!timedUnlock) {
			timedUnlock = true;
			yield return new WaitForSeconds (0.2f);
			objectInChest.GetComponent<ObjectInChest> ().ChestOpen = true;
		}
	}

}
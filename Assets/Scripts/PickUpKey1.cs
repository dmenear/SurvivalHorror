using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpKey1 : MonoBehaviour {

	public GameObject player, door, keyMesh, textBox, westExitTrigger, westEnterTrigger, monsterBlocker;
	public InteractiveHandler IH;
	public AudioClip pickup;
	public AudioSource audio;
	public bool pickedUp = false, delayPickup = false;
	public bool finalWestKey = false;

	// Update is called once per frame
	void Update () {
		if (GetComponent<ObjectInChest> () != null) {
			if(GetComponent<ObjectInChest>().ChestOpen){
				delayPickup = false;
			} else{
				delayPickup = true;
			}
		}
		if (IH.Interactable.Contains(keyMesh) && !pickedUp && !delayPickup) {
			if(Input.GetButtonDown("Action")) {
				audio.PlayOneShot (pickup, 0.7f);
				displayMessage ();
				if (door.GetComponent<DoorCellOpen> () == null) {
					door.GetComponent<VictoryDoor> ().DoorLocked = false;
				} else {
					door.GetComponent<DoorCellOpen> ().DoorLocked = false;
				}
				keyMesh.GetComponent<MeshRenderer> ().enabled = false;
				StartCoroutine (displayMessage());
				pickedUp = true;
				if (finalWestKey) {
					door.GetComponent<DoorCellOpen> ().SlamBehind = true;
					westExitTrigger.SetActive(true);
					monsterBlocker.SetActive (true);
					westEnterTrigger.SetActive (false);
				}
			}
		}
	}

	IEnumerator displayMessage(){
		textBox.GetComponent<Text> ().text = "You found a door key.";
		yield return new WaitForSeconds (3.0f);
		textBox.GetComponent<Text> ().text = "";
		gameObject.SetActive (false);
	}
}

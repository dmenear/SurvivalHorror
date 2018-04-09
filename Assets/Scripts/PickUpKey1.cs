using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpKey1 : MonoBehaviour {

	public GameObject player, door, keyMesh, textBox, westExitTrigger, westEnterTrigger, monsterBlocker;
	public AudioClip pickup;
	public AudioSource audio;
	public float distance, angle;
	public bool onFloor = false;
	public bool pickedUp = false;
	public bool finalWestKey = false;

	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.Angle (direction, player.transform.forward);
		distance = direction.magnitude;
		if (angle >= 160 && distance <= (onFloor ? 2.3f : 2f) && !pickedUp) {
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
		yield return new WaitForSecondsRealtime (3.0f);
		textBox.GetComponent<Text> ().text = "";
		gameObject.SetActive (false);
	}
}

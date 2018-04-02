using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpKey2 : MonoBehaviour {

	public GameObject player, textBox, monster, zoneExpansion, exitDoor, newDoor;
	public GameObject[] keyMeshes;
	public AudioClip pickup;
	public AudioSource audio;
	public float distance, angle;
	bool pickedUp = false;

	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.Angle (direction, player.transform.forward);
		distance = direction.magnitude;
		if (angle >= 160 && distance <= 3 && !pickedUp) {
			if(Input.GetButtonDown("Action")) {
				audio.PlayOneShot (pickup, 0.7f);
				displayMessage ();
				exitDoor.GetComponent<DoorCellOpen> ().DoorLocked = false;
				exitDoor.GetComponent<DoorCellOpen> ().DoorSlam = true;
				exitDoor.GetComponent<DoorCellOpen> ().complete = true;
				newDoor.GetComponent<DoorCellOpen> ().DoorLocked = false;
				foreach (GameObject keyMesh in keyMeshes) { 
					keyMesh.GetComponent<MeshRenderer> ().enabled = false;
				}
				StartCoroutine (displayMessage());
				pickedUp = true;
				monster.transform.position = monster.GetComponent<Monster2Controller> ().startPosition;
				zoneExpansion.SetActive (true);
			}
		}
	}

	IEnumerator displayMessage(){
		textBox.GetComponent<Text> ().text = "You found 2 keys.";
		yield return new WaitForSecondsRealtime (3.0f);
		textBox.GetComponent<Text> ().text = "";
		gameObject.SetActive (false);
	}
}

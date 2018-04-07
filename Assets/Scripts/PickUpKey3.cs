using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpKey3 : MonoBehaviour {

	public GameObject player, chest, keyMesh, textBox;
	public AudioClip pickup;
	public AudioSource audio;
	public float distance, angle;
	bool pickedUp = false, onFloor = false;

	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.Angle (direction, player.transform.forward);
		distance = direction.magnitude;
		if (angle >= 160 && distance <= (onFloor ? 3 : 2) && !pickedUp) {
			if(Input.GetButtonDown("Action")) {
				audio.PlayOneShot (pickup, 0.7f);
				displayMessage ();
				chest.GetComponent<ChestHandler> ().chestLocked = false;
				keyMesh.GetComponent<MeshRenderer> ().enabled = false;
				StartCoroutine (displayMessage());
				pickedUp = true;
			}
		}
	}

	IEnumerator displayMessage(){
		textBox.GetComponent<Text> ().text = "You found a chest key.";
		yield return new WaitForSecondsRealtime (3.0f);
		textBox.GetComponent<Text> ().text = "";
		gameObject.SetActive (false);
	}
}

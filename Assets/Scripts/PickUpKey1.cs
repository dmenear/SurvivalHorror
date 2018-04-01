using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey1 : MonoBehaviour {

	public GameObject player, door;
	public AudioClip pickup;
	public AudioSource audio;
	public float distance, angle;

	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.Angle (direction, player.transform.forward);
		distance = direction.magnitude;
		if (angle >= 160 && distance <= 2) {
			if(Input.GetButtonDown("Action")) {
				audio.PlayOneShot (pickup, 0.7f);
				door.GetComponent<DoorCellOpen> ().DoorLocked = false;
				gameObject.SetActive (false);
			}
		}
	}
}

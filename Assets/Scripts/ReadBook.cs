using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBook : MonoBehaviour {

	public GameObject player, bookPage;
	public float distance, angle;

	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.Angle (direction, player.transform.forward);
		distance = direction.magnitude;
		if (angle >= 160 && distance <= 2 && !bookPage.activeSelf) {
			if(Input.GetButtonDown("Action")) {
				bookPage.SetActive (true);
			}
		}
		else if (bookPage.activeSelf && (Input.GetButtonDown("Action") || Input.anyKeyDown)) {
			bookPage.SetActive (false);
		}
	}
}

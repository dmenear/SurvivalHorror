using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readflamethrowerscroll : MonoBehaviour {

	public GameObject player, flameThrowerScroll;
	public float distance, angle;

	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.Angle (direction, player.transform.forward);
		distance = direction.magnitude;
		if (angle >= 160 && distance <= 2 && !flameThrowerScroll.activeSelf) {
			if(Input.GetButtonDown("Action")) {
				flameThrowerScroll.SetActive (true);
			}
		}
		else if (flameThrowerScroll.activeSelf && (Input.GetButtonDown("Action") || Input.anyKeyDown)) {
			flameThrowerScroll.SetActive (false);
		}
	}
}

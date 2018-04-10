using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ControllerFixes : MonoBehaviour {

	public GameObject Player;
	public GameStateManager StateManager;
	public bool controllerUsed = false;

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("ControllerCheck1") != 0 || Input.GetAxis ("ControllerCheck2") != 0) {
			Player.GetComponent<FirstPersonController> ().m_MouseLook.controllerUsed = true;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			controllerUsed = true;
		} else if (Input.GetAxis ("MouseCheck1") != 0 || Input.GetAxis ("MouseCheck2") != 0) {
			Player.GetComponent<FirstPersonController> ().m_MouseLook.controllerUsed = false;
			controllerUsed = false;
		}
		if (controllerUsed) {
			if (Input.GetButtonDown ("Sprint")) {
				Player.GetComponent<FirstPersonController> ().sprint = true;
			}
			if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0) {
				Player.GetComponent<FirstPersonController> ().sprint = false;	
			}
		} else {
			if(Input.GetButtonDown("Sprint")){
				Player.GetComponent<FirstPersonController>().sprint = true;
			} else if(Input.GetButtonUp("Sprint")){
				Player.GetComponent<FirstPersonController>().sprint = false;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHandler : MonoBehaviour {

	public GameObject controlsPanel;
	public GameStateManager stateManager;

	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown (KeyCode.Tab) || Input.GetButtonDown("Menu")) && controlsPanel.activeSelf) {
			controlsPanel.SetActive (false);
		} else if (Input.GetKeyDown (KeyCode.Tab) && !controlsPanel.activeSelf && !stateManager.paused) {
			controlsPanel.SetActive (true);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsHandler : MonoBehaviour {

	public GameObject controlsPanel;
	public GameStateManager stateManager;

	void Update () {
		if ((Input.GetKeyDown (KeyCode.Tab) || Input.GetButtonDown("Menu")) && controlsPanel.activeSelf) {
			controlsPanel.SetActive (false);
		} else if (Input.GetKeyDown (KeyCode.Tab) && !controlsPanel.activeSelf && !stateManager.GamePaused) {
			controlsPanel.SetActive (true);
		}
	}
}

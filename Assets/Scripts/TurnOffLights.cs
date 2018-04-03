using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLights : MonoBehaviour {

	public GameObject editorLights;

	// Use this for initialization
	void Start () {
		editorLights.SetActive (false);
	}

}

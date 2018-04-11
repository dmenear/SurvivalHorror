using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public GameObject ThePlayer, FadeScreenIn, TextBox, Controls;
	
	void Start () {
		ThePlayer.GetComponent<FirstPersonController> ().enabled = false;
		StartCoroutine (scenePlayer());
	}

	IEnumerator scenePlayer(){
		yield return new WaitForSecondsRealtime (2.5f);
		FadeScreenIn.SetActive(false);
		ThePlayer.GetComponent<FirstPersonController> ().enabled = true;
		if (PlayerPrefs.GetInt ("Checkpoint1Reached") != 1) {
			Controls.SetActive (true);
		}
		TextBox.GetComponent<Text>().text = "";
	}
}

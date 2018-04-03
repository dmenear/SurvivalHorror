using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class AOpening : MonoBehaviour {

	public GameObject ThePlayer, FadeScreenIn, TextBox, Controls;
	
	// Update is called once per frame
	void Start () {
		ThePlayer.GetComponent<FirstPersonController> ().enabled = false;
		StartCoroutine (ScenePlayer());
	}

	IEnumerator ScenePlayer(){
		yield return new WaitForSeconds (2.5f);
		FadeScreenIn.SetActive(false);
		ThePlayer.GetComponent<FirstPersonController> ().enabled = true;
		Controls.SetActive (true);
		TextBox.GetComponent<Text>().text = "";
	}
}

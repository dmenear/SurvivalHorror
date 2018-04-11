using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointHandler : MonoBehaviour {

	public GameObject purpleCrystal, TextBox, Player, CP1;
	public GameObject CP1Monster;
	public Material purpleComplete;
	public bool checkPoint1Reached;
	public AudioClip CheckPointReached;
	bool checkPoint1Complete;
	bool animate1;
	
	void Start(){
		
		//PlayerPrefs.DeleteKey ("Checkpoint1Reached");
		if (PlayerPrefs.GetInt ("Checkpoint1Reached") == 1) {
			Player.transform.position = CP1.transform.position;
			Player.transform.rotation = CP1.transform.rotation;
			CP1Monster.SetActive (false);
			foreach (Transform item in purpleCrystal.transform) {
				animate1 = false;
				checkPoint1Reached = true;
				checkPoint1Complete = true;
				if (item.gameObject.GetComponent<Renderer> () != null) {
					item.gameObject.GetComponent<Renderer> ().material = purpleComplete;
				} else {
					item.gameObject.GetComponent<Light> ().intensity = 1.5f;
				}
			}
		} else {
			animate1 = true;
			checkPoint1Reached = false;
			checkPoint1Complete = false;
		}
	}

	void Update () {
		if (checkPoint1Reached) {
			foreach (Transform item in purpleCrystal.transform) {
				if (item.gameObject.GetComponent<Renderer> () != null) {
					item.gameObject.GetComponent<Renderer> ().material.Lerp (item.gameObject.GetComponent<Renderer> ().material, purpleComplete, 0.4f * Time.deltaTime);
				} else {
					item.gameObject.GetComponent<Light> ().intensity = Mathf.Lerp (item.gameObject.GetComponent<Light> ().intensity, 1.5f, 0.4f * Time.deltaTime);
				}
			}
			if (!checkPoint1Complete) {
				checkPoint1Complete = true;
				PlayerPrefs.SetInt ("Checkpoint1Reached", 1);
				GetComponent<AudioSource> ().PlayOneShot (CheckPointReached, 0.5f);
				StartCoroutine (displayText());
			}
		}
	}

	IEnumerator displayText(){
		TextBox.GetComponent<Text> ().text = "Checkpoint reached";
		yield return new WaitForSeconds (5.0f);
		TextBox.GetComponent<Text> ().text = "";
	}
}

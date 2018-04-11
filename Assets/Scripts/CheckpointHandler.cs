using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointHandler : MonoBehaviour {

	public GameObject purpleCrystal, greenCrystal, blueCrystal, TextBox, Player, CP1, CP2, CP3;
	public GameObject CP1Monster;
	public GameObject CP2Door, CP2DoorHinge, CP2NextDoor;
	public GameObject CP3Key, CP3Monster, CP3NextDoor;
	public Material purpleComplete, greenComplete, blueComplete;
	public bool checkPoint1Reached, checkPoint2Reached, checkPoint3Reached;
	public AudioClip CheckPointReached;
	bool checkPoint1Complete, checkPoint2Complete, checkPoint3Complete;
	bool animate1, animate2, animate3;
	
	void Start(){
		if (PlayerPrefs.GetInt ("Checkpoint1Reached") == 1) {
			Player.transform.position = CP1.transform.position;
			Player.transform.rotation = CP1.transform.rotation;
			CP1Monster.SetActive (false);
			animate1 = false;
			checkPoint1Reached = true;
			checkPoint1Complete = true;
			foreach (Transform item in purpleCrystal.transform) {
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
		if (PlayerPrefs.GetInt ("Checkpoint2Reached") == 1) {
			Player.transform.position = CP2.transform.position;
			Player.transform.rotation = CP2.transform.rotation;
			CP2Door.GetComponent<DoorCellOpen> ().DoorOpen = false;
			CP2Door.GetComponent<DoorCellOpen> ().DoorLocked = true;
			CP2DoorHinge.transform.localRotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
			CP2NextDoor.GetComponent<DoorCellOpen> ().DoorLocked = false;
			animate2 = false;
			checkPoint2Reached = true;
			checkPoint2Complete = true;
			foreach (Transform item in greenCrystal.transform) {
				if (item.gameObject.GetComponent<Renderer> () != null) {
					item.gameObject.GetComponent<Renderer> ().material = greenComplete;
				} else {
					item.gameObject.GetComponent<Light> ().intensity = 1.5f;
				}
			}
		} else {
			animate2 = true;
			checkPoint2Reached = false;
			checkPoint2Complete = false;
		}
		if (PlayerPrefs.GetInt ("Checkpoint3Reached") == 1) {
			Player.transform.position = CP3.transform.position;
			Player.transform.rotation = CP3.transform.rotation;
			CP2NextDoor.GetComponent<DoorCellOpen> ().DoorLocked = true;
			CP3Key.SetActive (false);
			CP3NextDoor.GetComponent<DoorCellOpen> ().DoorLocked = false;
			CP3Monster.SetActive (false);
			animate3 = false;
			checkPoint3Reached = true;
			checkPoint3Complete = true;
			foreach (Transform item in blueCrystal.transform) {
				if (item.gameObject.GetComponent<Renderer> () != null) {
					item.gameObject.GetComponent<Renderer> ().material = blueComplete;
				} else {
					item.gameObject.GetComponent<Light> ().intensity = 1.5f;
				}
			}
		} else {
			animate3 = true;
			checkPoint3Reached = false;
			checkPoint3Complete = false;
		}
	}

	void Update () {
		if (checkPoint1Reached && animate1) {
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
		if (checkPoint2Reached && animate2) {
			foreach (Transform item in greenCrystal.transform) {
				if (item.gameObject.GetComponent<Renderer> () != null) {
					item.gameObject.GetComponent<Renderer> ().material.Lerp (item.gameObject.GetComponent<Renderer> ().material, greenComplete, 0.4f * Time.deltaTime);
				} else {
					item.gameObject.GetComponent<Light> ().intensity = Mathf.Lerp (item.gameObject.GetComponent<Light> ().intensity, 1.5f, 0.4f * Time.deltaTime);
				}
			}
			if (!checkPoint2Complete) {
				checkPoint2Complete = true;
				PlayerPrefs.SetInt ("Checkpoint2Reached", 1);
				GetComponent<AudioSource> ().PlayOneShot (CheckPointReached, 0.5f);
				StartCoroutine (displayText());
			}
		}
		if (checkPoint3Reached && animate3) {
			foreach (Transform item in blueCrystal.transform) {
				if (item.gameObject.GetComponent<Renderer> () != null) {
					item.gameObject.GetComponent<Renderer> ().material.Lerp (item.gameObject.GetComponent<Renderer> ().material, blueComplete, 0.4f * Time.deltaTime);
				} else {
					item.gameObject.GetComponent<Light> ().intensity = Mathf.Lerp (item.gameObject.GetComponent<Light> ().intensity, 1.5f, 0.4f * Time.deltaTime);
				}
			}
			if (!checkPoint3Complete) {
				checkPoint3Complete = true;
				PlayerPrefs.SetInt ("Checkpoint3Reached", 1);
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

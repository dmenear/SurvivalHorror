using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCellOpen : MonoBehaviour {

	public float TheDistance;
	public GameObject TheDoor, TextBox;
	AudioSource audio;
	public AudioClip CreakSound, SlamSound, LockedSound;
	public bool DoorOpen = false, StateChanging = false, DoorSlam = false, DoorLocked = false, displayingText = false, slamBehind = false;

	void Start(){
		audio = TheDoor.GetComponent<AudioSource> ();
	}

	void Update () {
		TheDistance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver (){
		if (TheDistance <= 2.3 && !DoorOpen && !StateChanging) {
			if (Input.GetButtonDown ("Action")) {
				if (!DoorLocked) {
					TheDoor.GetComponent<Animation> ().Play ("FirstDoorOpenAnim");
					audio.PlayOneShot (CreakSound, 0.7f);
					StartCoroutine (SetStateChanging (1.5f, true));
				} else {
					if (!displayingText) {
						audio.PlayOneShot (LockedSound, 0.7f);
						StartCoroutine (DisplayDoorLocked ());
					}
				}
			}
		} else if(TheDistance <= 2.3 && DoorOpen && !StateChanging){
			if (Input.GetButtonDown ("Action")) {
				audio.PlayOneShot (CreakSound, 0.7f);
				if (DoorSlam) {
					StartCoroutine (PlaySlamSound ());
					TheDoor.GetComponent<Animation> ().Play ("DoorSlamAnim");
					StartCoroutine (SetStateChanging (0.5f, false));
				} else {
					TheDoor.GetComponent<Animation> ().Play ("FirstDoorCloseAnim");
					StartCoroutine (SetStateChanging (1.0f, false));
				}
			}
		}
	}

	IEnumerator SetStateChanging(float changeTime, bool newState) {
		StateChanging = true;
		yield return new WaitForSecondsRealtime (changeTime);
		StateChanging = false;
		DoorOpen = newState;
	}

	IEnumerator PlaySlamSound(){
		yield return new WaitForSecondsRealtime (0.45f);
		audio.PlayOneShot (SlamSound, 1.0f);
	}

	IEnumerator DisplayDoorLocked(){
		TextBox.GetComponent<Text> ().text = "This door is locked.";
		displayingText = true;
		yield return new WaitForSecondsRealtime (3.0f);
		displayingText = false;
		TextBox.GetComponent<Text> ().text = "";
	}

	public void SlamBehind(){
		StartCoroutine (PlaySlamSound ());
		TheDoor.GetComponent<Animation> ().Play ("DoorSlamAnim");
		StartCoroutine (SetStateChanging (0.5f, false));
	}
}

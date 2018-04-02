using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCellOpen : MonoBehaviour {

	public float TheDistance;
	public GameObject TheDoor, TextBox;
	public AudioSource CreakSound;
	public AudioClip SlamSound, LockedSound;
	public bool DoorOpen = false, StateChanging = false, DoorSlam = false, DoorLocked = false, displayingText = false;

	void Update () {
		TheDistance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver (){
		if (TheDistance <= 2.3 && !DoorOpen && !StateChanging) {
			if (Input.GetButtonDown ("Action")) {
				if (!DoorLocked) {
					TheDoor.GetComponent<Animation> ().Play ("FirstDoorOpenAnim");
					CreakSound.Play ();
					StartCoroutine (SetStateChanging (1.5f, true));
				} else {
					if (!displayingText) {
						CreakSound.PlayOneShot (LockedSound, 0.6f);
						StartCoroutine (DisplayDoorLocked ());
					}
				}
			}
		} else if(TheDistance <= 2.3 && DoorOpen && !StateChanging){
			if (Input.GetButtonDown ("Action")) {
				CreakSound.Play ();
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
		CreakSound.PlayOneShot (SlamSound, 1.0f);
	}

	IEnumerator DisplayDoorLocked(){
		TextBox.GetComponent<Text> ().text = "This door is locked.";
		displayingText = true;
		yield return new WaitForSecondsRealtime (3.0f);
		displayingText = false;
		TextBox.GetComponent<Text> ().text = "";
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCellOpen : MonoBehaviour {

	public float TheDistance;
	public GameObject TheDoor, TextBox, monster;
	AudioSource audio;
	public AudioClip CreakSound, SlamSound, LockedSound, PrisonOpen, PrisonClose;
	public bool DoorOpen = false, StateChanging = false, DoorSlam = false, DoorLocked = false, displayingText = false, slamBehind = false, isPrison = false, complete = false;
	public MusicController music;

	void Start(){
		audio = TheDoor.GetComponent<AudioSource> ();
	}

	void Update () {
		TheDistance = PlayerCasting.DistanceFromTarget;
	}

	void OnMouseOver (){
		if (TheDistance <= 2.5 && !DoorOpen && !StateChanging) {
			if (Input.GetButtonDown ("Action")) {
				if (!DoorLocked) {
					if (isPrison) {
						TheDoor.GetComponent<Animation> ().Play ("PrisonDoorOpen");
						audio.PlayOneShot (PrisonOpen, 0.7f);
						StartCoroutine (SetStateChanging (1.5f, true));
					} else {
						TheDoor.GetComponent<Animation> ().Play ("FirstDoorOpenAnim");
						audio.PlayOneShot (CreakSound, 0.7f);
						StartCoroutine (SetStateChanging (1.5f, true));
					}
				} else {
					if (!displayingText) {
						audio.PlayOneShot (LockedSound, 0.7f);
						StartCoroutine (DisplayDoorLocked ());
					}
				}
			}
		} else if(TheDistance <= 3.0 && DoorOpen && !StateChanging){
			if (Input.GetButtonDown ("Action")) {
				if (DoorSlam) {
					if (!CheckAttackZone1.inAttackZone) {
						audio.PlayOneShot (CreakSound, 0.7f);
						StartCoroutine (PlaySlamSound ());
						TheDoor.GetComponent<Animation> ().Play ("DoorSlamAnim");
						StartCoroutine (SetStateChanging (0.5f, false));
						if (complete) {
							GetComponent<DoorCellOpen> ().enabled = false;
							StartCoroutine (killMonster ());
						}
					}
				} else {
					if (!slamBehind){
						if (isPrison) {
							TheDoor.GetComponent<Animation> ().Play ("PrisonDoorClose");
							audio.PlayOneShot (PrisonClose, 0.7f);
							StartCoroutine (SetStateChanging (1.0f, false));
						} else {
							audio.PlayOneShot (CreakSound, 0.7f);
							TheDoor.GetComponent<Animation> ().Play ("FirstDoorCloseAnim");
							StartCoroutine (SetStateChanging (1.0f, false));
						}
					}
				}
			}
		}
	}

	IEnumerator killMonster(){
		yield return new WaitForSecondsRealtime (0.5f);
		music.changeToAmbient ();
		Destroy (monster);
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

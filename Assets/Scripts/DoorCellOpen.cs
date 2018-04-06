using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCellOpen : MonoBehaviour {

	public float TheDistance, angle;
	public GameObject TheDoor, TextBox, monster, player;
	AudioSource audio;
	public AudioClip CreakSound, SlamSound, LockedSound, PrisonOpen, PrisonClose;
	public bool DoorOpen = false, StateChanging = false, DoorSlam = false, DoorLocked = false, displayingText = false, slamBehind = false, isPrison = false, complete = false;
	public MusicController music;
	bool openedOpposite;

	void Start(){
		audio = TheDoor.GetComponent<AudioSource> ();
		openedOpposite = false;
	}

	void Update () {
		TheDistance = PlayerCasting.DistanceFromTarget;
		Vector3 direction = player.transform.position - this.transform.position;
		angle = Vector3.SignedAngle (direction, this.transform.forward, Vector3.up);
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
						if (angle < 0) {
							TheDoor.GetComponent<Animation> ().Play ("FirstDoorOpenAnim");
							openedOpposite = false;
						} else {
							TheDoor.GetComponent<Animation> ().Play ("DoorOpenOpposite");
							openedOpposite = true;
						}
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
						if (openedOpposite) {
							TheDoor.GetComponent<Animation> ().Play ("DoorSlamOpposite");
						} else {	
							TheDoor.GetComponent<Animation> ().Play ("DoorSlamAnim");
						}
						StartCoroutine (SetStateChanging (0.5f, false));
						if (complete) {
							StartCoroutine (killMonster ());
							GetComponent<DoorCellOpen> ().DoorLocked = true;

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
							if (openedOpposite) {
								TheDoor.GetComponent<Animation> ().Play ("DoorCloseOpposite");
							} else {
								TheDoor.GetComponent<Animation> ().Play ("FirstDoorCloseAnim");
							}
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

	void SetLayerRecursively(GameObject obj, int layer){
		obj.layer = layer;
		foreach(Transform child in obj.transform){
			SetLayerRecursively (child.gameObject, layer);
		}
	}
}

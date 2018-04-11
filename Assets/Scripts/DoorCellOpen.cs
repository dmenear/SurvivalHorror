using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorCellOpen : MonoBehaviour {

	public float Distance, Angle;
	public GameObject Door, TextBox, Player;
	public InteractiveHandler IH;
	public FlamethrowerControls FTControls;

	public AudioClip CreakSound, SlamSound, LockedSound, PrisonOpen, PrisonClose;
	public bool DoorOpen = false, 
		StateChanging = false,
		DoorSlam = false,
		DoorLocked = false,
		DisplayingText = false,
		SlamBehind = false,
		IsPrison = false,
		Complete = false,
		SkeletonDoor = false,
		openedOpposite = false,
		westMazeEntrance = false;
	public MusicController music;

	AudioSource audio;

	void Start(){
		audio = Door.GetComponent<AudioSource> ();
	}

	void Update () {
		Vector3 direction = Player.transform.position - this.transform.position;
		Angle = Vector3.SignedAngle (direction, this.transform.forward, Vector3.up);
		if (IH.Interactable.Contains(this.gameObject) && !DoorOpen && !StateChanging) {
			if (Input.GetButtonDown ("Action")) {
				if (!DoorLocked) {
					if (IsPrison) {
						Door.GetComponent<Animation> ().Play ("PrisonDoorOpen");
						audio.PlayOneShot (PrisonOpen, 0.7f);
						StartCoroutine (SetStateChanging (1.5f, true));
					} else {
						if (westMazeEntrance) {
							StartCoroutine(FTControls.Disassemble ());
							westMazeEntrance = false;
						}
						if (SkeletonDoor) {
							music.turnOffMusic ();
						}
						if (Angle < 0) {
							Door.GetComponent<Animation> ().Play ("FirstDoorOpenAnim");
							openedOpposite = false;
						} else {
							Door.GetComponent<Animation> ().Play ("DoorOpenOpposite");
							openedOpposite = true;
						}
						audio.PlayOneShot (CreakSound, 0.7f);
						StartCoroutine (SetStateChanging (1.5f, true));
					}
				} else {
					if (!DisplayingText) {
						audio.PlayOneShot (LockedSound, 0.7f);
						StartCoroutine (DisplayDoorLocked ());
					}
				}
			}
		} else if(IH.Interactable.Contains(this.gameObject) && DoorOpen && !StateChanging){
			if (Input.GetButtonDown ("Action")) {
				if (DoorSlam) {
					if (!CheckSpiderZone2.InZone) {
						audio.PlayOneShot (CreakSound, 0.7f);
						StartCoroutine (PlaySlamSound ());
						if (openedOpposite) {
							Door.GetComponent<Animation> ().Play ("DoorSlamOpposite");
						} else {	
							Door.GetComponent<Animation> ().Play ("DoorSlamAnim");
						}
						StartCoroutine (SetStateChanging (0.5f, false));
						if (Complete) {
							StartCoroutine (endMusic ());
							GetComponent<DoorCellOpen> ().DoorLocked = true;
							if (GetComponent<DoorCellOpen> ().SlamBehind) {
								GetComponent<DoorCellOpen> ().SlamBehind = false;
							}
						}
					}
				} else {
					if (!SlamBehind){
						if (IsPrison) {
							Door.GetComponent<Animation> ().Play ("PrisonDoorClose");
							audio.PlayOneShot (PrisonClose, 0.7f);
							StartCoroutine (SetStateChanging (1.0f, false));
						} else {
							audio.PlayOneShot (CreakSound, 0.7f);
							if (openedOpposite) {
								Door.GetComponent<Animation> ().Play ("DoorCloseOpposite");
							} else {
								Door.GetComponent<Animation> ().Play ("FirstDoorCloseAnim");
							}
							StartCoroutine (SetStateChanging (1.0f, false));
						}
					}
				}
			}
		}
	}

	public IEnumerator endMusic(){
		yield return new WaitForSecondsRealtime (0.5f);
		music.changeToAmbient ();
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
		DisplayingText = true;
		yield return new WaitForSecondsRealtime (3.0f);
		DisplayingText = false;
		TextBox.GetComponent<Text> ().text = "";
	}

	public void SlamDoorBehind(){
		StartCoroutine (PlaySlamSound ());
		if (openedOpposite) {
			Door.GetComponent<Animation> ().Play ("DoorSlamOpposite");
		} else {
			Door.GetComponent<Animation> ().Play ("DoorSlamAnim");
		}
		StartCoroutine (SetStateChanging (0.5f, false));
	}

	void SetLayerRecursively(GameObject obj, int layer){
		obj.layer = layer;
		foreach(Transform child in obj.transform){
			SetLayerRecursively (child.gameObject, layer);
		}
	}

	public void CloseDoor(){
		if (openedOpposite) {
			Door.GetComponent<Animation> ().Play ("DoorCloseOpposite");
		} else {
			Door.GetComponent<Animation> ().Play ("FirstDoorCloseAnim");
		}
		StartCoroutine (SetStateChanging (1.0f, false));
	}
}

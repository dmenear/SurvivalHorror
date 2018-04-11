using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpKey2 : MonoBehaviour {

	public GameObject player, textBox, monster, zoneExpansion, exitZone, exitDoor, newDoor, skeleton, scareTrigger;
	public GameObject[] keyMeshes, torches, torchLights, torchFlames;
	public InteractiveHandler IH;
	public AudioClip pickup;
	public AudioSource audio;
	public bool pickedUp = false;

	// Update is called once per frame
	void Update () {
		if ((IH.Interactable.Contains(keyMeshes[0])||IH.Interactable.Contains(keyMeshes[1])) && !pickedUp) {
			if(Input.GetButtonDown("Action")) {
				audio.PlayOneShot (pickup, 0.7f);
				displayMessage ();
				exitDoor.GetComponent<DoorCellOpen> ().DoorLocked = false;
				exitDoor.GetComponent<DoorCellOpen> ().SlamBehind = true;
				exitDoor.GetComponent<DoorCellOpen> ().Complete = true;
				newDoor.GetComponent<DoorCellOpen> ().DoorLocked = false;
				foreach (GameObject keyMesh in keyMeshes) { 
					keyMesh.GetComponent<MeshRenderer> ().enabled = false;
				}
				StartCoroutine (displayMessage());
				pickedUp = true;
				monster.transform.position = monster.GetComponent<Monster2Controller> ().StartPosition;
				zoneExpansion.SetActive (true);
				exitZone.SetActive (true);
				foreach (GameObject torch in torches) {
					torch.GetComponent<FlameAnimations> ().enabled = false;
				}
				foreach (GameObject torchFlame in torchFlames) {
					torchFlame.GetComponent<ParticleSystem> ().Stop ();
				}
				foreach (GameObject torchLight in torchLights) {
					torchLight.GetComponent<Animation> ().Play ("BurnOut");
				}
				skeleton.SetActive (true);
				scareTrigger.SetActive (true);
			}
		}
	}

	IEnumerator displayMessage(){
		textBox.GetComponent<Text> ().text = "You found 2 door keys.";
		yield return new WaitForSeconds (3.0f);
		textBox.GetComponent<Text> ().text = "";
		gameObject.SetActive (false);
	}
}

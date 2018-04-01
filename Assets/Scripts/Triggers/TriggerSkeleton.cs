using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSkeleton : MonoBehaviour {

	public GameObject player, skeleton, flashlight, skeleTorch, skeleTorchLight, skeleTorchFlame, book;
	public AudioClip flicker;
	public bool skeletonTriggered = false;

	void OnTriggerEnter () {
		if (!skeletonTriggered) {
			skeletonTriggered = true;
			if (flashlight.activeSelf) {
				flashlight.GetComponent<Animation> ().Play ("FlashlightFlicker");
				player.GetComponents<AudioSource>()[1].PlayOneShot (flicker, 0.7f);
			}
			skeleton.SetActive (false);
			book.SetActive (true);
			StartCoroutine (startTorchAnimation ());
		}
	}

	IEnumerator startTorchAnimation(){
		yield return new WaitForSeconds (0.5f);
		skeleTorchFlame.SetActive (true);
		skeleTorchLight.GetComponent<Animation> ().Play ("LightTorch");
		yield return new WaitForSeconds (1.5f);
		skeleTorch.GetComponent<FlameAnimations> ().enabled = true;
	}
}

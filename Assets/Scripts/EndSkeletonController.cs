using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSkeletonController : MonoBehaviour {

	public GameObject Neck, Chest, Head, Jaw, Player, Flashlight, StateManager;
	public AudioClip Flicker;
	public float Angle;
	public Vector3 direction, currentRotation, newRotation;
	bool snap, triggered;

	// Use this for initialization
	void Start () {
		snap = false;
		triggered = false;
	}

	// Update is called once per frame
	void Update () {
		direction = Player.transform.position - this.transform.position;
		direction.y = 0;
		float angle = Vector3.Angle ((direction * -1f), this.transform.right);
		if (angle <= 20.0f) {
			angle = 20.0f;
		} else if(angle >= 160.0f){
			angle = 160.0f;
		}
		angle -= 90;
		if (transform.InverseTransformPoint(Player.transform.position).y >= 0) {
			if (angle >= 0) {
				angle = 70.0f;
			} else if (angle < 0) {
				angle = -70.0f;
			}
		}
		changeRotation (Neck, angle, 0.6f);
		changeRotation (Chest, angle, 0.4f);
		if (snap) {
			snapHead ();
		}
	}

	void changeRotation(GameObject bodyPart, float angle, float modifier){
		angle = angle * modifier;
		currentRotation = bodyPart.transform.localEulerAngles;
		if (currentRotation.z > 180.0f) {
			currentRotation.z = (360.0f - currentRotation.z) * -1.0f;
		}
		newRotation = new Vector3 (bodyPart.transform.localEulerAngles.x, bodyPart.transform.localEulerAngles.y, angle);
		bodyPart.transform.localEulerAngles = Vector3.Lerp(currentRotation, newRotation, 2.0f * Time.deltaTime);
	}

	void snapHead(){
		currentRotation = Neck.transform.localEulerAngles;
		newRotation = new Vector3(0.0f, Neck.transform.localEulerAngles.y, Neck.transform.localEulerAngles.z);
		Neck.transform.localEulerAngles = Vector3.Lerp(currentRotation, newRotation, 10.0f * Time.deltaTime);
		currentRotation = Head.transform.localEulerAngles;
		newRotation = new Vector3(0.0f, Head.transform.localEulerAngles.y, Head.transform.localEulerAngles.z);
		Head.transform.localEulerAngles = Vector3.Lerp(currentRotation, newRotation, 10.0f * Time.deltaTime);
		currentRotation = Jaw.transform.localEulerAngles;
		newRotation = new Vector3(40.0f, Jaw.transform.localEulerAngles.y, Jaw.transform.localEulerAngles.z);
		Jaw.transform.localEulerAngles = Vector3.Lerp(currentRotation, newRotation, 7.0f * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject == Player.gameObject) {
			snap = true;
			if (!triggered) {
				StartCoroutine (snapSequence ());
				triggered = true;
			}
		}
	}

	IEnumerator snapSequence(){
		if (Flashlight.activeSelf) {
			Flashlight.GetComponent<Animation> ().Play ("FlashlightFlicker");
			StateManager.GetComponent<AudioSource>().PlayOneShot (Flicker, 0.7f);
		}
		yield return new WaitForSeconds(0.28f);
		Destroy (gameObject);
	}
}

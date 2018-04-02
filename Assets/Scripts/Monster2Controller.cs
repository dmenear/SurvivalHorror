using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2Controller : MonoBehaviour {

	Animator anim;
	public GameObject player, flashlight;
	string state;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		state = "idle";
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.transform.position - this.transform.position;
		direction.y = 0;
		this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
		if (flashlight.activeSelf && !anim.GetBool ("isChasing") && CheckAttackZone.inAttackZone) {
			anim.SetBool ("isChasing", true);
			state = "chase";
		} else if ((!flashlight.activeSelf && anim.GetBool ("isChasing")) || !CheckAttackZone.inAttackZone) {
			anim.SetBool ("isChasing", false);
			state = "idle";
		}
		if (state == "chase") {
			this.transform.Translate (0, 0, 10f * Time.deltaTime);
		}
	}
}

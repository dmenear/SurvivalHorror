using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveHandler : MonoBehaviour {

	public List<GameObject> Interactable;

	void Start () {
		Interactable = new List<GameObject> ();
	}

	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		Interactable.Add (other.gameObject);
	}

	void OnTriggerExit(Collider other){
		Interactable.Remove (other.gameObject);
	}
}

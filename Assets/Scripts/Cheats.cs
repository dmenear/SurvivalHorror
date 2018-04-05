using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

	public GameObject[] monsters;
	public GameObject flamethrower;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.K)){
			foreach(GameObject m in monsters){
				Destroy (m);
			}
		}
		if(Input.GetKeyDown(KeyCode.F) && !flamethrower.activeSelf){
			flamethrower.SetActive (true);
		} else if(Input.GetKeyDown(KeyCode.F) && flamethrower.activeSelf){
			flamethrower.SetActive (false);
		}
	}
}

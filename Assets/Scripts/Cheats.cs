using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour {

	public GameObject[] Monsters;
	public GameObject Flamethrower;

	void Update () {
		//Kill all monsters
		if(Input.GetKeyDown(KeyCode.K)){
			foreach(GameObject m in Monsters){
				Destroy (m);
			}
		}

		//Give or take flamethrower
		if(Input.GetKeyDown(KeyCode.F) && !Flamethrower.activeSelf){
			Flamethrower.SetActive (true);
		} else if(Input.GetKeyDown(KeyCode.F) && Flamethrower.activeSelf){
			Flamethrower.SetActive (false);
		}
	}
}

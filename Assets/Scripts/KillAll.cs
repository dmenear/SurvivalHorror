﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAll : MonoBehaviour {

	public GameObject[] monsters;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
			foreach(GameObject m in monsters){
				Destroy (m);
			}

		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameStateManager : MonoBehaviour {

	public GameObject monsters, player;
	public bool paused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EndGame(){
		monsters.SetActive (false);
		player.GetComponent<FirstPersonController> ().enabled = false;
		StartCoroutine (restart ());
	}

	public void Victory(){
	
	}

	IEnumerator restart(){
		yield return new WaitForSeconds (8.1f);
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}

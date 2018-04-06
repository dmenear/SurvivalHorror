using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public GameObject FadeOut;

	void Start(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void StartGame(){
		StartCoroutine (FadeOutStart ());
	}

	public void Exit(){
		Application.Quit ();
	}

	IEnumerator FadeOutStart(){
		FadeOut.SetActive (true);
		FadeOut.GetComponent<Animation> ().Play ();
		yield return new WaitForSeconds (2.0f);
		SceneManager.LoadScene ("Scene001");
	}
}

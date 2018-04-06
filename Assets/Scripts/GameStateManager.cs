using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameStateManager : MonoBehaviour {

	public GameObject Player, FadeOut, VictoryText;
	public GameObject[] Monsters;
	public bool GamePaused;

	void Start(){
		GamePaused = false;
	}

	public void EndGame(){
		foreach (GameObject m in Monsters) {
			m.SetActive (false);
		}
		Player.GetComponent<FirstPersonController> ().enabled = false;
		StartCoroutine (restart ());
	}

	public void Victory(){
		foreach (GameObject m in Monsters) {
			m.SetActive (false);
		}
		Player.GetComponent<FirstPersonController> ().enabled = false;
		FadeOut.SetActive (true);
		FadeOut.GetComponent<Animation> ().Play ("FadeScreenOut");
		StartCoroutine (showVictoryText ());
		StartCoroutine (backToMenu ());
	}

	IEnumerator restart(){
		yield return new WaitForSecondsRealtime (8.1f);
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	IEnumerator backToMenu(){
		yield return new WaitForSecondsRealtime (15.0f);
		SceneManager.LoadScene ("MainMenu");
	}

	IEnumerator showVictoryText(){
		yield return new WaitForSecondsRealtime (2.5f);
		VictoryText.SetActive (true);
	}
}

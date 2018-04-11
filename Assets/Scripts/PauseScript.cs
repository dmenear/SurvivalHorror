using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseScript : MonoBehaviour
{

    public GameObject pauseMenu, player;
	public GameStateManager stateManager;
	public ControllerFixes cFixes;
	public bool paused;

	void Start(){
		if (!cFixes.controllerUsed) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		paused = false;
	}

    void Update()
    {
		if (paused && !cFixes.controllerUsed) {
			SetCursorLockState (false);
		}
        if (Input.GetButtonDown("Menu") && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            player.GetComponent<FirstPersonController>().enabled = false;
			stateManager.GamePaused = true;
			paused = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Menu") && pauseMenu.activeSelf)
        {
			Time.timeScale = 1;
			player.GetComponent<FirstPersonController>().enabled = true;
            pauseMenu.SetActive(false);
			stateManager.GamePaused = false;
			paused = false;
			SetCursorLockState (true);
        }
    }

	void SetCursorLockState(bool val){
		if (val) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void Resume(){
		Time.timeScale = 1;
		player.GetComponent<FirstPersonController>().enabled = true;
		pauseMenu.SetActive(false);
		stateManager.GamePaused = false;
		paused = false;
		SetCursorLockState (true);
	}

    public void Restart()
    {
        Time.timeScale = 1;
		stateManager.GamePaused = false;
		CheckSpiderZone1.InZone = false;
		CheckSpiderZone2.InZone = false;
		CheckZombieZone.SafelyInZone = false;
		PlayerPrefs.DeleteKey ("Checkpoint1Reached");
		PlayerPrefs.DeleteKey ("Checkpoint2Reached");
		PlayerPrefs.DeleteKey ("Checkpoint3Reached");
        SceneManager.LoadScene("Scene001");
    }

	public void RestartCheckpoint()
	{
		Time.timeScale = 1;
		stateManager.GamePaused = false;
		CheckSpiderZone1.InZone = false;
		CheckSpiderZone2.InZone = false;
		CheckZombieZone.SafelyInZone = false;
		SceneManager.LoadScene("Scene001");
	}

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
		stateManager.GamePaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

}

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

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

    void Update()
    {
        if (Input.GetButtonDown("Menu") && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            player.GetComponent<FirstPersonController>().enabled = false;
			stateManager.paused = true;
			SetCursorLockState (false);
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Menu") && pauseMenu.activeSelf)
        {
			Time.timeScale = 1;
			player.GetComponent<FirstPersonController>().enabled = true;
            pauseMenu.SetActive(false);
			stateManager.paused = false;
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
		stateManager.paused = false;
		SetCursorLockState (true);
	}

    public void Restart()
    {
        Time.timeScale = 1;
		stateManager.paused = false;
		CheckAttackZone.inAttackZone = false;
		CheckAttackZone1.inAttackZone = false;
		CheckSafetyZone1.inSafetyZone1 = false;
        SceneManager.LoadScene("Scene001");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
		stateManager.paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

}

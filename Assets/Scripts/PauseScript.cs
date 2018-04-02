using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseScript : MonoBehaviour
{

    public GameObject pauseMenu, player;

    private void Update()
    {
        if (Input.GetButtonDown("Menu") && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else if (Input.GetButtonDown("Menu") && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            player.GetComponent<FirstPersonController>().enabled = true;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Scene001");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

}

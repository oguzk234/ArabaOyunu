using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused=false;

    public GameObject pauseMenuUI;
    public GameObject StatsCanvas;
    public GameObject optionsMenu;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        StatsCanvas.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        StatsCanvas.SetActive(false );
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Options()
    {
        print("OptionsOnDevelopment");
        /*
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
        */
    }

    public void LoadMenu()

    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

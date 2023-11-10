using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused=false;

    public GameObject pauseMenuUI;
    public GameObject timerBar;
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
        //timerBar.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        //timerBar.SetActive(false );
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsMenu.SetActive(true);
        print("XXX");
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

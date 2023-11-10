using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume",volume);

    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Back()
    {
        print("Back!");
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}

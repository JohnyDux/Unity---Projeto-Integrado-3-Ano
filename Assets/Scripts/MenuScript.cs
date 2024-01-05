using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public SettingsMenu settingsRef;
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
        settingsRef.SetResolution(settingsRef.current_resolution);
        settingsRef.SetFullscreen(settingsRef.fullscreen);
        settingsRef.SetQuality(settingsRef.current_quality);
        settingsRef.SetMasterVolume(settingsRef.current_masterVolume);
        settingsRef.SetMusicVolume(settingsRef.current_musicVolume);
        settingsRef.SetSoundFXVolume(settingsRef.current_soundFXVolume);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public SettingsMenu settings;
    public void FixedUpdate()
    {
        settings.SetResolution(settings.current_resolution);
        settings.SetFullscreen(settings.fullscreen);
        settings.SetQuality(settings.current_quality);
        settings.SetVolume(settings.current_volume);  
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

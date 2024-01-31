using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public SettingsMenu settingsRef;
    string fullPath;

    private void Start()
    {
        LoadFromJson();

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Intro")
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadFromJson()
    {
        string folderPath = "SettingsData";
        fullPath = Path.Combine(Application.dataPath, folderPath);

        string json = File.ReadAllText(fullPath + "/SettingsData.json");
        SettingsData data = JsonUtility.FromJson<SettingsData>(json);

        settingsRef.current_resolution = data.resolutionId;
        settingsRef.current_quality = data.graphicsId;
        settingsRef.current_masterVolume = data.masterVolumeValue;
        settingsRef.current_musicVolume = data.musicValue;
        settingsRef.current_soundFXVolume = data.soundFxValue;
        settingsRef.fullscreen = data.fullscreenValue;
    }
}

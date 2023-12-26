using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    public JsonReadWriteSystem jsonRead;

    Resolution[] resolutions;

    public int current_resolution;
    public bool fullscreen;
    public int current_quality;
    public float current_masterVolume;
    public float current_musicVolume;
    public float current_soundFXVolume;

    void Start()
    {
        resolutions = Screen.resolutions;

        if(resolutionDropdown != null)
        {
            resolutionDropdown.ClearOptions();
        }

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        if (resolutionDropdown != null)
        {
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        current_resolution = resolutionIndex;
    }

    public void SetMasterVolume (float volume)
    {
        audioMixer.SetFloat("Master", volume);
        current_masterVolume = volume;
    }
    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("Music", volume);
        current_musicVolume = volume;
    }
    public void SetSoundFXVolume (float volume)
    {
        audioMixer.SetFloat("SoundEffects", volume);
        current_soundFXVolume = volume;
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        current_quality = qualityIndex;
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        fullscreen = isFullscreen;
    }
}

using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JsonReadWriteSystem : MonoBehaviour
{

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown graphicsDropdown;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    public void SaveToJson()
    {
        SettingsData data = new SettingsData();
        data.resolution_id = resolutionDropdown.value;
        data.quality_id = graphicsDropdown.value;
        data.volume_value = volumeSlider.value;
        data.isFullscreen = fullscreenToggle.enabled;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/SettingsDataFile.json", json);
    }

    public void LoadFromJson()
    {
        string json = File.ReadAllText(Application.dataPath + "/SettingsDataFile.json");
        SettingsData data = JsonUtility.FromJson<SettingsData>(json);
        resolutionDropdown.value = data.resolution_id;
        graphicsDropdown.value = data.quality_id;
        volumeSlider.value = data.volume_value;
        fullscreenToggle.enabled = data.isFullscreen;
    }

}

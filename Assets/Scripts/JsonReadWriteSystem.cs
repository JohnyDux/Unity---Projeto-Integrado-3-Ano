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

    string fullPath;
    public void SaveToJson()
    {
        SettingsData data = new SettingsData();
        data.resolutionId = resolutionDropdown.value;
        data.graphicsId = graphicsDropdown.value;
        data.volumeValue = volumeSlider.value;
        data.fullscreenValue = fullscreenToggle.isOn;

        string folderPath = "SettingsData";

        string json = JsonUtility.ToJson(data, true);
        fullPath = Path.Combine(Application.dataPath, folderPath);
        File.WriteAllText(fullPath + "/SettingsData.json", json);
    }

    public void LoadFromJson()
    {
        string folderPath = "SettingsData";
        fullPath = Path.Combine(Application.dataPath, folderPath);

        string json = File.ReadAllText(fullPath+ "/SettingsData.json");
        SettingsData data = JsonUtility.FromJson<SettingsData>(json);

        resolutionDropdown.value = data.resolutionId;
        graphicsDropdown.value = data.graphicsId;
        volumeSlider.value = data.volumeValue;
        fullscreenToggle.isOn = data.fullscreenValue;
    }

}

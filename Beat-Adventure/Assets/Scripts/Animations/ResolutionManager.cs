using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResolutionSettings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        resolutionDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRateRatio.value == Screen.currentResolution.refreshRateRatio.value)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        LoadSettings();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetResolution(int index)
    {
        if (index < 0 || index >= resolutions.Length) return;

        Resolution selectedResolution = resolutions[index];

        FullScreenMode fullscreenMode = Screen.fullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;


        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenMode, selectedResolution.refreshRateRatio);

        PlayerPrefs.SetInt("ResolutionIndex", index);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int savedIndex = PlayerPrefs.GetInt("ResolutionIndex");
            if (savedIndex >= 0 && savedIndex < resolutions.Length)
            {
                currentResolutionIndex = savedIndex;
                resolutionDropdown.value = currentResolutionIndex;
                resolutionDropdown.RefreshShownValue();
                SetResolution(currentResolutionIndex);
            }
        }

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;
            Screen.fullScreenMode = isFullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        }
        else
        {
            fullscreenToggle.isOn = Screen.fullScreen;
        }
    }
}

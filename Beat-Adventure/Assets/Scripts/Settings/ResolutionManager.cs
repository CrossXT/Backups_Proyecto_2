using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ResolutionManager : MonoBehaviour

{
    public Dropdown resolutionDropdown;

    Resolution[] resolutions;
    int currentResolutionIndex = 0;

    void Start()
    {
        resolutions = Screen.resolutions.Select(r => new Resolution { width = r.width, height = r.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        var options = resolutions.Select(r => r.width + " x " + r.height).ToList();
        resolutionDropdown.AddOptions(options);

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}


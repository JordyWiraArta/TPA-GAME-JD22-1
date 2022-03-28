using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionScript : MonoBehaviour
{

    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutionList;
    // Start is called before the first frame update
    void Start()
    {
        resolutionList = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        int i = 0;
        List<string> options = new List<string>();
        foreach (Resolution resolution in resolutionList)
        {
            options.Add(resolution.width + "x" + resolution.height);

            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            i++;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void resolutionChange(int resolutionIndex)
    {
        Resolution resolution = resolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}

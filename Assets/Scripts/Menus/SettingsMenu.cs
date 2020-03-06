using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionsSelector;
    public Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionsSelector.ClearOptions();
        List<string> options = new List<string>();
        int CurrentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolution = i;
            }
        }
        resolutionsSelector.AddOptions(options);
        resolutionsSelector.value = CurrentResolution;
        resolutionsSelector.RefreshShownValue();
    }
    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetFullScreen(bool FoolScreenBool)
    {
        Screen.fullScreen = FoolScreenBool;
    }
    public void ChangeResolution(int resolution)
    {
        Screen.SetResolution(resolutions[resolution].width, resolutions[resolution].height, Screen.fullScreen);
    }
}

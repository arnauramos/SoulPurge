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
    public Toggle fullscreenSelector;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionsSelector.ClearOptions();
        List<string> options = new List<string>();
        int CurrentResolution = 0;
        for (int i = resolutions.Length-1; i > -1; i--)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolution = i;
            }
        }
        resolutionsSelector.AddOptions(options);
        resolutionsSelector.value = resolutions.Length - 1 - CurrentResolution;
        resolutionsSelector.RefreshShownValue();
        fullscreenSelector.isOn = Screen.fullScreen;
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
        int i = resolutions.Length - 1 - resolution;
        Screen.SetResolution(resolutions[i].width, resolutions[i].height, Screen.fullScreen);
    }
}

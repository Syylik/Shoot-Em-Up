using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Video")]

    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _qualityDrowdown;

    private Resolution[] _resolutions;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private readonly string _fullscreenSave = "Fullscreen";
    private readonly string _qualitySave = "Quality";
    private readonly string _resolutionSave = "Resolution";

    [Header("Audio")]

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private readonly string _musicParam = "Music";
    private readonly string _soundParam = "Sound";



    private void Start()
    {
        bool fullscreenState = PlayerPrefs.GetInt(_fullscreenSave, 1) == 1 ? true : false;
        _fullscreenToggle.isOn = fullscreenState;
        SetFullscreen(fullscreenState);

        _qualityDrowdown.value = PlayerPrefs.GetInt(_qualitySave, QualitySettings.GetQualityLevel());
        SetQuality(_qualityDrowdown.value);

        _qualityDrowdown.RefreshShownValue();

        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int curResolutionIndex = 0;
        
        for(int i = 0; i < _resolutions.Length; i++)
        {
            string option = $"{_resolutions[i].width} x {_resolutions[i].height}";
            options.Add(option);

            if
            (_resolutions[i].width == Screen.currentResolution.width &&
            _resolutions[i].height == Screen.currentResolution.height) curResolutionIndex = i;
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = curResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        _musicSlider.value = GetVolumeSave(_musicParam);
        _soundSlider.value = GetVolumeSave(_soundParam);
    }

    public void SetFullscreen(bool state)
    {
        Screen.fullScreen = state;
        PlayerPrefs.SetInt(_fullscreenSave, state ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt(_qualitySave, index);
        PlayerPrefs.Save();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(_resolutionSave, index);
        PlayerPrefs.Save();
    }

    private float GetVolumeSave(string param)
    {
        var volumePercent = PlayerPrefs.GetFloat(param, 10);
        _mixer.SetFloat(param, GetVolume(volumePercent));
        return volumePercent;
    }

    public void SetMusicVolume(float volumePercent)
    {
        var volume = GetVolume(volumePercent);
        _mixer.SetFloat(_musicParam, volume);
        PlayerPrefs.SetFloat(_musicParam, volumePercent);
        PlayerPrefs.Save();
    }

    public void SetSoundVolume(float volumePercent)
    {
        var volume = GetVolume(volumePercent);
        _mixer.SetFloat(_soundParam, volume);
        PlayerPrefs.SetFloat(_soundParam, volumePercent);
        PlayerPrefs.Save();
    }

    private float GetVolume(float volumePercent) { return Mathf.Lerp(-45f, 0f, volumePercent / 10f); }  
}
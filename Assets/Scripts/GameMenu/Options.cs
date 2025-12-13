using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Video")]

    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private PostProcessVolume _postProcessVolume;

    private readonly string _qualitySave = "Quality";

    [SerializeField] private GameObject _fpsCounter;


    [Header("Audio")]

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private readonly string _musicParam = "Music";
    private readonly string _soundParam = "Sound";



    private void Start()
    {
        _qualityDropdown.value = PlayerPrefs.GetInt(_qualitySave, QualitySettings.GetQualityLevel());
        SetQuality(_qualityDropdown.value);

        _qualityDropdown.RefreshShownValue();

        _musicSlider.value = GetVolumeSave(_musicParam) / 10;
        _soundSlider.value = GetVolumeSave(_soundParam) / 10;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt(_qualitySave, index);
        PlayerPrefs.Save();

        _postProcessVolume.enabled = index >= 2 ? true : false;
    }

    public void SetFpsCounter(bool state) => _fpsCounter.SetActive(state);

    private float GetVolumeSave(string param)
    {
        var volumePercent = PlayerPrefs.GetFloat(param, 50);
        _mixer.SetFloat(param, GetVolume(volumePercent));
        return volumePercent;
    }

    public void SetMusicVolume(float volumePercent) => SetAudioVolume(volumePercent * 10, _musicParam);

    public void SetSoundVolume(float volumePercent) => SetAudioVolume(volumePercent * 10, _soundParam); 

    public void SetAudioVolume(float volumePercent, string audioParam)
    {
        if(volumePercent < 1) volumePercent = .001f;

        var volume = GetVolume(volumePercent);
        _mixer.SetFloat(audioParam, volume);
        PlayerPrefs.SetFloat(audioParam, volumePercent);
        PlayerPrefs.Save();
    }

    private float GetVolume(float volumePercent) { return Mathf.Log10(volumePercent / 100f) * 40f; }  
}
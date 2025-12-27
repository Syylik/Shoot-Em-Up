using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [Header("Video")]
    [SerializeField] private Toggle _postProcessToggle;
    [SerializeField] private PostProcessVolume _postProcessVolume;

    private readonly string _postProcessSave = "PostProcess";

    [SerializeField] private GameObject _fpsCounter;


    [Header("Audio")]

    [SerializeField] private AudioMixer _mixer;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private readonly string _musicParam = "Music";
    private readonly string _soundParam = "Sound";

    private void Start()
    {
        _postProcessToggle.isOn = PlayerPrefs.GetInt(_postProcessSave, 0) == 1 ? true : false;
        _postProcessVolume.enabled = _postProcessToggle.isOn;

        _musicSlider.value = GetVolumeSave(_musicParam) / 10;
        _soundSlider.value = GetVolumeSave(_soundParam) / 10;
    }

    public void TogglePostProcess(bool state)
    {
        _postProcessVolume.enabled = state;
        PlayerPrefs.SetInt(_postProcessSave, state ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleFullscreen(bool state) => Screen.fullScreen = state;

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
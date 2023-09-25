using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _sliderVolumeMusic;
    [SerializeField] private Slider _sliderVolumeGame;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _dropdownGraphics;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private TMP_Dropdown _dropdownResolutions;

    private void Start()
    {

        _dropdownResolutions.ClearOptions();
        _dropdownResolutions.AddOptions(GameManager.Instance.Options);
        _dropdownResolutions.value = GameManager.Instance.ResolutionIndex;
        _dropdownResolutions.RefreshShownValue();
        SetResolution(GameManager.Instance.ResolutionIndex);

        _sliderVolumeMusic.value = GameManager.Instance.MusicVolumeValue;
        SetVolumeMusic(GameManager.Instance.MusicVolumeValue);

        _dropdownGraphics.value = GameManager.Instance.IndexQuality;
        SetQuality(GameManager.Instance.IndexQuality);

        _toggle.isOn = GameManager.Instance.IsFullscreen;
        SetFullscreen(GameManager.Instance.IsFullscreen);
    }

    private void SetVolumeMusic(float value)
    {
        _audioMixer.SetFloat("MusicVolume", value);
        GameManager.Instance.MusicVolumeValue = value;
    }

    private void SetVolumeGame(float value)
    {
        _audioMixer.SetFloat("GameVolume", value);
        GameManager.Instance.GameVolumeValue = value;
    }

    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        GameManager.Instance.IndexQuality = qualityIndex;
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        GameManager.Instance.IsFullscreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        (int, int) resolution = GameManager.Instance.Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.Item1, resolution.Item2, Screen.fullScreen);
        GameManager.Instance.ResolutionIndex = resolutionIndex;
    }

    private void OnEnable()
    {
        _sliderVolumeMusic.onValueChanged.AddListener(SetVolumeMusic);
        _sliderVolumeGame.onValueChanged.AddListener(SetVolumeGame);
        _dropdownGraphics.onValueChanged.AddListener(SetQuality);
        _toggle.onValueChanged.AddListener(SetFullscreen);
        _dropdownResolutions.onValueChanged.AddListener(SetResolution);
    }

    private void OnDisable()
    {
        _sliderVolumeMusic.onValueChanged.RemoveListener(SetVolumeMusic);
        _sliderVolumeGame.onValueChanged.RemoveListener(SetVolumeGame);
        _dropdownGraphics.onValueChanged.RemoveListener(SetQuality);
        _toggle.onValueChanged.RemoveListener(SetFullscreen);
        _dropdownResolutions.onValueChanged.RemoveListener(SetResolution);
    }
}

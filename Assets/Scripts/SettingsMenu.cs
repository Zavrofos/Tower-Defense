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
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _dropdownResolutions.ClearOptions();
        _dropdownResolutions.AddOptions(_gameManager.Options);
        _dropdownResolutions.value = _gameManager.ResolutionIndex;
        _dropdownResolutions.RefreshShownValue();
        SetResolution(_gameManager.ResolutionIndex);

        _sliderVolumeMusic.value = _gameManager.MusicVolumeValue;
        SetVolumeMusic(_gameManager.MusicVolumeValue);

        _dropdownGraphics.value = _gameManager.IndexQuality;
        SetQuality(_gameManager.IndexQuality);

        _toggle.isOn = _gameManager.IsFullscreen;
        SetFullscreen(_gameManager.IsFullscreen);
    }

    private void SetVolumeMusic(float value)
    {
        _audioMixer.SetFloat("MusicVolume", value);
        _gameManager.MusicVolumeValue = value;
    }

    private void SetVolumeGame(float value)
    {
        _audioMixer.SetFloat("GameVolume", value);
        _gameManager.GameVolumeValue = value;
    }

    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        _gameManager.IndexQuality = qualityIndex;
    }

    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        _gameManager.IsFullscreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        (int, int) resolution = _gameManager.Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.Item1, resolution.Item2, Screen.fullScreen);
        _gameManager.ResolutionIndex = resolutionIndex;
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

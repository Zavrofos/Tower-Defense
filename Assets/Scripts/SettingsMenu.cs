using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using SaveSystemDir;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _sliderVolumeMusic;
    [SerializeField] private Slider _sliderVolumeGame;
    [SerializeField] private Button _closeButton;
    [SerializeField] private AudioMixer _audioMixer;

    private void Awake()
    {
        _sliderVolumeMusic.value = SaveSystem.GetVolumeMusic();
        _sliderVolumeGame.value = SaveSystem.GetVolumeGame();
        _sliderVolumeMusic.onValueChanged.AddListener(SetVolumeMusic);
        _sliderVolumeGame.onValueChanged.AddListener(SetVolumeGame);
        _closeButton.onClick.AddListener(Close);
    }
    
    private void SetVolumeMusic(float value)
    {
        _audioMixer.SetFloat("MusicVolume", FormatToDb(value));
        SaveSystem.SaveVolumeMusicScreen(value);
    }

    private void SetVolumeGame(float value)
    {
        _audioMixer.SetFloat("GameVolume", FormatToDb(value));
        SaveSystem.SaveVolumeGameScreen(value);
    }

    private float FormatToDb(float value01)
    {
        value01 = Mathf.Clamp(value01, 0.0001f, 1f);
        return Mathf.Log10(value01) * 20f;
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _sliderVolumeMusic.onValueChanged.RemoveAllListeners();
        _sliderVolumeGame.onValueChanged.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }
}

using System;
using Cysharp.Threading.Tasks;
using SaveSystemDir;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private AudioMixer _audioMixer;

    private async UniTask Awake()
    {
        await UniTask.Yield();
        
        _audioMixer.SetFloat("MusicVolume", FormatToDb(SaveSystem.GetVolumeMusic()));
        _audioMixer.SetFloat("GameVolume", FormatToDb(SaveSystem.GetVolumeGame()));
    }

    private void OnPlay()
    {
        SceneManager.LoadScene("GameHub");
    }

    private void OnOpenOptions()
    {
        _settingsMenu.gameObject.SetActive(true);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlay);
        _settingsButton.onClick.AddListener(OnOpenOptions);
        _quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlay);
        _settingsButton.onClick.RemoveListener(OnOpenOptions);
        _quitButton.onClick.RemoveListener(OnQuit);
    }
    
    private float FormatToDb(float value01)
    {
        value01 = Mathf.Clamp(value01, 0.0001f, 1f);
        return Mathf.Log10(value01) * 20f;
    }

}

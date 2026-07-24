using System;
using Assets.Scripts;
using Assets.Scripts.UIScripts;
using Cysharp.Threading.Tasks;
using SaveSystemDir;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioClip _clickAudio;

    private void Awake()
    {
        Observable.NextFrame()
            .Subscribe(_ =>
            {
                _audioMixer.SetFloat("MusicVolume", FormatToDb(SaveSystem.GetVolumeMusic()));
                _audioMixer.SetFloat("GameVolume", FormatToDb(SaveSystem.GetVolumeGame()));
                LocalizationSystem.Instance.SetLanguage(SaveSystem.GetCurrentLanguage());
            })
            .AddTo(this);
    }
    
    private void OnPlay()
    {
        ClickSoundPlayGlobal.Instance.Play(_clickAudio);
        SceneManager.LoadScene("GameHub");
    }

    private void OnOpenOptions()
    {
        ClickSoundPlayGlobal.Instance.Play(_clickAudio);
        _settingsMenu.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlay);
        _settingsButton.onClick.AddListener(OnOpenOptions);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
    }

    private float FormatToDb(float value01)
    {
        value01 = Mathf.Clamp(value01, 0.0001f, 1f);
        return Mathf.Log10(value01) * 20f;
    }

}

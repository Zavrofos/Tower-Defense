using System;
using Assets.Scripts.UIScripts;
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
    [SerializeField] private AudioClip _clickAudio;

    private void Awake()
    {
        _audioMixer.SetFloat("MusicVolume", FormatToDb(SaveSystem.GetVolumeMusic()));
        _audioMixer.SetFloat("GameVolume", FormatToDb(SaveSystem.GetVolumeGame()));
    }

    private bool _playClicked;
    private bool _isDestroed;
    
    private async UniTask OnPlay()
    {
        if(_playClicked)
            return;

        ClickSoundPlayGlobal.Instance.Play(_clickAudio);
        await UniTask.Delay(TimeSpan.FromSeconds(_clickAudio.length));
        
        if(_isDestroed)
            return;
        
        SceneManager.LoadScene("GameHub");
    }

    private void OnOpenOptions()
    {
        ClickSoundPlayGlobal.Instance.Play(_clickAudio);
        _settingsMenu.gameObject.SetActive(true);
    }

    private void OnQuit()
    {
        ClickSoundPlayGlobal.Instance.Play(_clickAudio);
        Application.Quit();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(() => OnPlay().Forget());
        _settingsButton.onClick.AddListener(OnOpenOptions);
        _quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    private void OnDestroy()
    {
        _isDestroed = true;
    }

    private float FormatToDb(float value01)
    {
        value01 = Mathf.Clamp(value01, 0.0001f, 1f);
        return Mathf.Log10(value01) * 20f;
    }

}

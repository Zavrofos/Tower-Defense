using System;
using Assets.Scripts;
using Assets.Scripts.UIScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private AudioSource _audioSource;
    
    [field: SerializeField] public TMP_Text ReveardText { get; private set; }

    private void Awake()
    {
        _continueButton.onClick.AddListener(Continue);
        _mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void Continue()
    {
        ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.CLickAudioUI);
        GameManager.Instance.ObjectPooler.ClearPool();
        SceneManager.UnloadSceneAsync($"GameLevel_{GameManager.Instance.CurrentWorld}_{GameManager.Instance.CurrentLevel}");
        GameManager.Instance.GameHub.gameObject.SetActive(true);
        GameManager.Instance.GameOverlay.gameObject.SetActive(false);
        GameManager.Instance.SetNormalSpeedGame();

        if (MenuMusicPlayer.Instance != null)
            MenuMusicPlayer.Instance.PlayMusic();

        gameObject.SetActive(false);
    }

    private void MainMenu()
    {
        ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.CLickAudioUI);

        if (MenuMusicPlayer.Instance != null)
            MenuMusicPlayer.Instance.PlayMusic();

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    
    public void PlaySound(AudioClip clip)
    {
        if (!clip)
            return;

        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
    }

}

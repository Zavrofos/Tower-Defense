using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GlobalShop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeOptionsButton;
    [SerializeField] private Button _closeLevelsMenuButton;

    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _optionsMenu;

    private void OnPlay()
    {
        CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;

        if (currentGameData.IsWinGame || currentGameData.IsWinLevel || currentGameData.IsGameOverLevel)
        {
            SceneManager.LoadScene("StartGameScene");
            return;
        }
        
        SceneManager.LoadScene($"GameLevel{GameManager.Instance.CurrentGameData.CurrentLevel}");
    }

    private void OnOpenOptions()
    {
        _optionsMenu.SetActive(true);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnCloseLevelsMenu()
    {
        _levelsMenu.SetActive(false);
    }

    private void OnCloseOptions()
    {
        _optionsMenu.SetActive(false);
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlay);
        _optionsButton.onClick.AddListener(OnOpenOptions);
        _quitButton.onClick.AddListener(OnQuit);
        _closeLevelsMenuButton.onClick.AddListener(OnCloseLevelsMenu);
        _closeOptionsButton.onClick.AddListener(OnCloseOptions);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlay);
        _optionsButton.onClick.RemoveListener(OnOpenOptions);
        _quitButton.onClick.RemoveListener(OnQuit);
        _closeLevelsMenuButton.onClick.RemoveListener(OnCloseLevelsMenu);
        _closeOptionsButton.onClick.RemoveListener(OnCloseOptions);
    }

}

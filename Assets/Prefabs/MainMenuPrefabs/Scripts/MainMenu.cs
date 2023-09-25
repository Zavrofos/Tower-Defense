using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _levelsButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeOptionsButton;
    [SerializeField] private Button _closeLevelsMenuButton;

    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _optionsMenu;

    private void OnPlay()
    {
        GameManager.Instance.CurrentLevel = 1;
        SceneManager.LoadScene("GameLevel1");
    }

    private void OnOpenLevelsMenu()
    {
        _levelsMenu.SetActive(true);
    }

    private void OnOpenOptions()
    {
        _optionsMenu.SetActive(true);
    }

    private void OnQuit()
    {
        Debug.Log("Quit!!!");
        SaveSystem.SaveLevels(LevelsManager.Instance.Levels);
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
        _levelsButton.onClick.AddListener(OnOpenLevelsMenu);
        _optionsButton.onClick.AddListener(OnOpenOptions);
        _quitButton.onClick.AddListener(OnQuit);
        _closeLevelsMenuButton.onClick.AddListener(OnCloseLevelsMenu);
        _closeOptionsButton.onClick.AddListener(OnCloseOptions);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlay);
        _levelsButton.onClick.RemoveListener(OnOpenLevelsMenu);
        _optionsButton.onClick.RemoveListener(OnOpenOptions);
        _quitButton.onClick.RemoveListener(OnQuit);
        _closeLevelsMenuButton.onClick.RemoveListener(OnCloseLevelsMenu);
        _closeOptionsButton.onClick.RemoveListener(OnCloseOptions);
    }

}

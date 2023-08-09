using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PouseMenu : MonoBehaviour
{
    [SerializeField] private GameManagerInGame _gameManager;
    [SerializeField] private Button _selectALevelButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeWindowButton;
    [SerializeField] private Button _closeLevelsMenuButton;
    [SerializeField] private Button _closeOptionsMenuButton;
    
    [SerializeField] private GameObject _pouseMenu;
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _optionsMenu;

    public bool IsPouse;
    private LevelsManager _levelsManager;
    private GameManagerInGame _gameManagerInGame;
    private AudioManager _audioManager;

    private void Start()
    {
        _levelsManager = FindObjectOfType<LevelsManager>();
        _gameManagerInGame = FindObjectOfType<GameManagerInGame>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnOpenLevelsMenu()
    {
        _levelsMenu.SetActive(true);
        _pouseMenu.SetActive(false);
    }

    private void OnOpenOptionsMenu()
    {
        _optionsMenu.SetActive(true);
        _pouseMenu.SetActive(false);
    }

    private void OnBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    private void OnQuit()
    {
        SaveSystem.SaveLevels(_levelsManager.Levels);
        Application.Quit();
    }

    private void OnCloseWindow()
    {
        gameObject.SetActive(false);
        _gameManagerInGame.IsDisableButtonColliders = false;
        _gameManagerInGame.IsPouse = false;
        _audioManager.PlayAudio();
        Time.timeScale = 1;
    }

    private void OnCloseLevelsMenu()
    {
        _levelsMenu.SetActive(false);
        _pouseMenu.SetActive(true);
    }

    private void OnCloseOptionsMenu()
    {
        _optionsMenu.SetActive(false);
        _pouseMenu.SetActive(true);
    }


    private void OnEnable()
    {
        _selectALevelButton.onClick.AddListener(OnOpenLevelsMenu);
        _optionsButton.onClick.AddListener(OnOpenOptionsMenu);
        _mainMenuButton.onClick.AddListener(OnBackToMainMenu);
        _quitButton.onClick.AddListener(OnQuit);
        _closeWindowButton.onClick.AddListener(OnCloseWindow);
        _closeLevelsMenuButton.onClick.AddListener(OnCloseLevelsMenu);
        _closeOptionsMenuButton.onClick.AddListener(OnCloseOptionsMenu);
        IsPouse = true;
        _levelsMenu.SetActive(false);
        _optionsMenu.SetActive(false);
        _pouseMenu.SetActive(true);
    }

    private void OnDisable()
    {
        _selectALevelButton.onClick.RemoveListener(OnOpenLevelsMenu);
        _optionsButton.onClick.RemoveListener(OnOpenOptionsMenu);
        _mainMenuButton.onClick.RemoveListener(OnBackToMainMenu);
        _quitButton.onClick.RemoveListener(OnQuit);
        _closeWindowButton.onClick.RemoveListener(OnCloseWindow);
        _closeLevelsMenuButton.onClick.RemoveListener(OnCloseLevelsMenu);
        _closeOptionsMenuButton.onClick.RemoveListener(OnCloseOptionsMenu);
        IsPouse = false;
    }
}

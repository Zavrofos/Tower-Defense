using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PouseMenu : MonoBehaviour
{
    [SerializeField] private GameManagerInGame _gameManager;
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
    
    private GameManagerInGame _gameManagerInGame;
    

    private void Start()
    {
        _gameManagerInGame = FindObjectOfType<GameManagerInGame>();
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
        Application.Quit();
    }

    private void OnCloseWindow()
    {
        gameObject.SetActive(false);
        _gameManagerInGame.IsDisableButtonColliders = false;
        _gameManagerInGame.IsPouse = false;
        AudioManager.Instance.PlayAudio();
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
        _optionsButton.onClick.RemoveListener(OnOpenOptionsMenu);
        _mainMenuButton.onClick.RemoveListener(OnBackToMainMenu);
        _quitButton.onClick.RemoveListener(OnQuit);
        _closeWindowButton.onClick.RemoveListener(OnCloseWindow);
        _closeLevelsMenuButton.onClick.RemoveListener(OnCloseLevelsMenu);
        _closeOptionsMenuButton.onClick.RemoveListener(OnCloseOptionsMenu);
        IsPouse = false;
    }
}

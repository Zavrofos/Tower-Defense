using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GlobalShop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _resetGameButton;
    [SerializeField] private Button _resetLevelButton;
    [SerializeField] private Button _buyContinueGame;
    [SerializeField] private Button _globalShopButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeLevelsMenuWindow;

    [SerializeField] private GameObject _gameOverWindow;
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private Transform _levelsConteiner;
    [SerializeField] private GlobalShop _globalShop;
    
    private void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameLevel" + GameManager.Instance.CurrentGameData.CurrentLevel);
    }

    private void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameLevel1");
    }

    private void OpenGlobalShop()
    {
        _gameOverWindow.SetActive(false);
        _globalShop.gameObject.SetActive(true);
        _globalShop.CloseButton.onClick.AddListener(ShowGameOverMenu);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void CloseWindowLevelsMenu()
    {
        ShowGameOverMenu();
        _gameOverWindow.SetActive(true);
        _levelsMenu.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        _gameOverWindow.SetActive(true);
        _globalShop.CloseButton.onClick.RemoveAllListeners();
    }

    private void OnEnable()
    {
        _resetLevelButton.onClick.AddListener(ResetLevel);
        _resetGameButton.onClick.AddListener(ResetGame);
        _globalShopButton.onClick.AddListener(OpenGlobalShop);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
        _closeLevelsMenuWindow.onClick.AddListener(CloseWindowLevelsMenu);
    }

    private void OnDisable()
    {
        _resetLevelButton.onClick.RemoveListener(ResetLevel);
        _resetGameButton.onClick.RemoveListener(ResetGame);
        _globalShopButton.onClick.RemoveListener(OpenGlobalShop);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
        _closeLevelsMenuWindow.onClick.RemoveListener(CloseWindowLevelsMenu);
    }

}

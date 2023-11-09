using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Dev.DevScripts.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _selectALevelButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeLevelsMenuWindow;

    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private Transform _levelsConteiner;

    private GameManagerInGame _gameManagerInGame;

    private void Awake()
    {
        _gameManagerInGame = FindObjectOfType<GameManagerInGame>();
    }

    public void OpenNextLevel()
    {
        _winWindow.SetActive(true);
        _gameManagerInGame.IsDisableButtonColliders = true;
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        GameManager.Instance.CurrentLevel++;
        SceneManager.LoadScene("GameLevel" + GameManager.Instance.CurrentLevel);
        Time.timeScale = 1;
    }

    public void SelectALevel()
    {
        _winMenu.SetActive(false);
        _levelsMenu.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void CloseWindowLevelsMenu()
    {
        _winMenu.SetActive(true);
        _levelsMenu.SetActive(false);
    }



    private void OnEnable()
    {
        _selectALevelButton.onClick.AddListener(SelectALevel);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
        _closeLevelsMenuWindow.onClick.AddListener(CloseWindowLevelsMenu);
    }

    private void OnDisable()
    {
        _selectALevelButton.onClick.RemoveListener(SelectALevel);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
        _closeLevelsMenuWindow.onClick.RemoveListener(CloseWindowLevelsMenu);
    }
}

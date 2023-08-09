using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private LevelView _levelViewPrefab;

    [HideInInspector] public Dictionary<string, LevelView> LevelsViews = new Dictionary<string, LevelView>();
    private GameManagerInGame _gameManagerInGame;
    private GameManager _gameManager;
    private LevelsManager _levelManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManagerInGame = FindObjectOfType<GameManagerInGame>();
        _levelManager = FindObjectOfType<LevelsManager>();
        for (int i = 0; i < _levelManager.Levels.Count; i++)
        {
            Level level = _levelManager.Levels[i];
            LevelView levelPref = Instantiate(_levelViewPrefab, _levelsConteiner);
            levelPref.Level = level;
            levelPref.LabelText.text = level.Label;
            if (level.IsOpen) levelPref.OpenLevel();
            LevelsViews.Add(levelPref.LabelText.text, levelPref);
        }
    }

    public void OpenNextLevel()
    {
        if(_gameManager.CurrentLevel < _levelManager.CountLevels)
        {
            _levelManager.Levels[_gameManager.CurrentLevel].OpenLevel();
        }
        _winWindow.SetActive(true);
        _gameManagerInGame.IsDisableButtonColliders = true;
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        if (_gameManager.CurrentLevel >= _levelManager.CountLevels)
        {
            _winWindow.SetActive(true);
            return;
        }
        _gameManager.CurrentLevel++;
        SceneManager.LoadScene("GameLevel" + _gameManager.CurrentLevel);
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
        SaveSystem.SaveLevels(_levelManager.Levels);
        Application.Quit();
    }

    public void CloseWindowLevelsMenu()
    {
        _winMenu.SetActive(true);
        _levelsMenu.SetActive(false);
    }



    private void OnEnable()
    {
        if(_gameManager.CurrentLevel < _levelManager.CountLevels)
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
        }
        _selectALevelButton.onClick.AddListener(SelectALevel);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
        _closeLevelsMenuWindow.onClick.AddListener(CloseWindowLevelsMenu);
    }

    private void OnDisable()
    {
        if (_gameManager.CurrentLevel < _levelManager.CountLevels)
        {
            _nextLevelButton.onClick.RemoveListener(NextLevel);
        }
        _selectALevelButton.onClick.RemoveListener(SelectALevel);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
        _closeLevelsMenuWindow.onClick.RemoveListener(CloseWindowLevelsMenu);
    }
}

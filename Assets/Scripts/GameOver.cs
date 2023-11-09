using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Dev.DevScripts.SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _resetLevelButton;
    [SerializeField] private Button _selectALevelButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeLevelsMenuWindow;

    [SerializeField] private GameObject _gameOverWindow;
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private Transform _levelsConteiner;
    [SerializeField] private LevelView _levelViewPrefab;

    private List<LevelView> levels = new List<LevelView>();

    private void Start()
    {
        for (int i = 0; i < LevelsManager.Instance.Levels.Count; i++)
        {
            Level level = LevelsManager.Instance.Levels[i];
            LevelView levelPref = Instantiate(_levelViewPrefab, _levelsConteiner);
            levelPref.Level = level;
            levelPref.LabelText.text = level.Label;
            if (level.IsOpen) levelPref.OpenLevel();
        }
    }
    private void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameLevel" + GameManager.Instance.CurrentLevel);
    }

    private void SelectALevel()
    {
        _gameOverWindow.SetActive(false);
        _levelsMenu.SetActive(true);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    private void Quit()
    {
        SaveSystem.SaveLevels(LevelsManager.Instance.Levels);
        Application.Quit();
    }

    private void CloseWindowLevelsMenu()
    {
        _gameOverWindow.SetActive(true);
        _levelsMenu.SetActive(false);
        foreach(var level in levels)
        {
            Destroy(level.gameObject);
        }
        levels.Clear();
    }

    private void OnEnable()
    {
        _resetLevelButton.onClick.AddListener(ResetLevel);
        _selectALevelButton.onClick.AddListener(SelectALevel);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
        _closeLevelsMenuWindow.onClick.AddListener(CloseWindowLevelsMenu);
    }

    private void OnDisable()
    {
        _resetLevelButton.onClick.RemoveListener(ResetLevel);
        _selectALevelButton.onClick.RemoveListener(SelectALevel);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
        _closeLevelsMenuWindow.onClick.RemoveListener(CloseWindowLevelsMenu);
    }

}

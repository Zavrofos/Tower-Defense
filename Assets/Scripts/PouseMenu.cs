using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PouseMenu : MonoBehaviour
{
    [SerializeField] private GameManagerInGame _gameManager;
    public Button SelectALevelButton;
    public Button OptionsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeWindowButton;
    [SerializeField] private Button _closeOptionsMenuButton;
    
    public GameObject PouseMenuWindow;

    public bool IsPouse;
    
    private GameManagerInGame _gameManagerInGame;
    

    private void Start()
    {
        _gameManagerInGame = FindObjectOfType<GameManagerInGame>();
    }

    private void OnBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    private void OnQuit()
    {
        SaveSystem.SaveLevels(LevelsManager.Instance.Levels);
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

    private void OnEnable()
    {
        _mainMenuButton.onClick.AddListener(OnBackToMainMenu);
        _quitButton.onClick.AddListener(OnQuit);
        _closeWindowButton.onClick.AddListener(OnCloseWindow);
        IsPouse = true;
        PouseMenuWindow.SetActive(true);
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(OnBackToMainMenu);
        _quitButton.onClick.RemoveListener(OnQuit);
        _closeWindowButton.onClick.RemoveListener(OnCloseWindow);
        IsPouse = false;
    }
}

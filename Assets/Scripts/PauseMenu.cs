using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _closeWindowButton;

    public bool IsPause { get; set; }

    private void Awake()
    {
        _settingsButton.onClick.AddListener(OnOpenSettingsMenu);
        _mainMenuButton.onClick.AddListener(OnBackToMainMenu);
        _quitButton.onClick.AddListener(OnQuit);
        _closeWindowButton.onClick.AddListener(OnCloseWindow);
        gameObject.SetActive(true);
    }

    private void OnOpenSettingsMenu()
    {
        GameManager.Instance.SettingsMenu.gameObject.SetActive(true);
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
        GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = false;
        Time.timeScale = GameManager.Instance.CurrentSpeedGame;
    }

    private void OnDestroy()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
        _closeWindowButton.onClick.RemoveAllListeners();
    }
}

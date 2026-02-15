using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;

    private void Continue()
    {
        SceneManager.UnloadSceneAsync($"GameLevel_{GameManager.Instance.CurrentWorld}_{GameManager.Instance.CurrentLevel}");
        GameManager.Instance.GameHub.gameObject.SetActive(true);
        GameManager.Instance.GameOverlay.gameObject.SetActive(false);
        Time.timeScale = 1;
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

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(Continue);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(Continue);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
    }
}

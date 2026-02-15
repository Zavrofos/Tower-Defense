using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _continueButton.onClick.AddListener(Continue);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
    }

    private void Continue()
    {
        GameManager.Instance.ObjectPooler.ClearPool();
        SceneManager.UnloadSceneAsync($"GameLevel_{GameManager.Instance.CurrentWorld}_{GameManager.Instance.CurrentLevel}");
        GameManager.Instance.GameHub.gameObject.SetActive(true);
        GameManager.Instance.GameOverlay.gameObject.SetActive(false);
        GameManager.Instance.SetNormalSpeedGame();
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

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

}

using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGameMenu : MonoBehaviour
{
    [field: SerializeField] public Button StartNewGameButton { get; private set; }
    [field: SerializeField] public Button MainMenuButton { get; private set; }
    [field: SerializeField] public Button QuitGameButton { get; private set; }

    private void StartNewGame()
    {
        GameManager.Instance.CurrentGameData = new CurrentGameData();
        GameManager.Instance.CurrentGameData.Init();
        SaveSystem.SaveSystem.SaveGame();
        SceneManager.LoadScene("GameLevel1");
    }

    private void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void QutGame()
    {
        Application.Quit();
    }
        
    private void OnEnable()
    {
        StartNewGameButton.onClick.AddListener(StartNewGame);
        MainMenuButton.onClick.AddListener(OpenMainMenu);
        QuitGameButton.onClick.AddListener(QutGame);
    }

    private void OnDisable()
    {
        StartNewGameButton.onClick.RemoveListener(StartNewGame);
        MainMenuButton.onClick.RemoveListener(OpenMainMenu);
        QuitGameButton.onClick.RemoveListener(QutGame);
    }
}
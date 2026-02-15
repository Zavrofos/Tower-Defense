using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using SaveSystemDir;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGameMenu : MonoBehaviour
{
    [field: SerializeField] public Button ContinueGameButton { get; private set; }
    [field: SerializeField] public Button MainMenuButton { get; private set; }
    [field: SerializeField] public Button QuitGameButton { get; private set; }

    private void Continue()
    {
        
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
        ContinueGameButton.onClick.AddListener(Continue);
        MainMenuButton.onClick.AddListener(OpenMainMenu);
        QuitGameButton.onClick.AddListener(QutGame);
    }

    private void OnDisable()
    {
        ContinueGameButton.onClick.RemoveListener(Continue);
        MainMenuButton.onClick.RemoveListener(OpenMainMenu);
        QuitGameButton.onClick.RemoveListener(QutGame);
    }
}
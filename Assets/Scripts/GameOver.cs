using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;
    
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
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
    }

}

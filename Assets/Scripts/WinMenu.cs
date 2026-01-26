using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private Transform _levelsConteiner;
    [SerializeField] private GlobalShop _globalShop;
    
    public void NextLevel()
    {
        SceneManager.LoadScene("GameLevel" + GameManager.Instance.CurrentGameData.CurrentLevel);
        Time.timeScale = 1;
    }

    public void OpenGlobalShop()
    {
        _winMenu.SetActive(false);
        _globalShop.gameObject.SetActive(true);
        _globalShop.CloseButton.onClick.AddListener(ShowWinMenu);
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

    public void ShowWinMenu()
    {
        if(GameManager.Instance.CurrentGameManagerLevel)
            GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = true;
        _globalShop.CloseButton.onClick.RemoveAllListeners();
        _globalShop.gameObject.SetActive(false);
        _winMenu.SetActive(true);
    }

    private void OnEnable()
    {
        if(GameManager.Instance.CurrentGameData.CurrentLevel <= GameManager.Instance.CountLevels)
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
        }
        
        _shopButton.onClick.AddListener(OpenGlobalShop);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        if (GameManager.Instance.CurrentGameData.CurrentLevel < GameManager.Instance.CountLevels)
        {
            _nextLevelButton.onClick.RemoveListener(NextLevel);
        }
        _shopButton.onClick.RemoveListener(OpenGlobalShop);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
    }
}

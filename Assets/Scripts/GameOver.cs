using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button _resetGameButton;
    [SerializeField] private Button _resetLevelButton;
    [SerializeField] private BuyContinueGameButton _buyContinueGameButton;
    [SerializeField] private Button _globalShopButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private GameObject _gameOverWindow;
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private Transform _levelsConteiner;
    [SerializeField] private GlobalShop _globalShop;
    
    private void ResetLevel()
    {
        Time.timeScale = 1;
        GameManager.Instance.CurrentGameData.ResetLevelBought = false;
        SaveSystem.SaveSystem.SaveGame();
        SceneManager.LoadScene("GameLevel" + GameManager.Instance.CurrentGameData.CurrentLevel);
    }

    private void ResetGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.CurrentGameData.CurrentLevel = 1;
        SaveSystem.SaveSystem.SaveGame();
        SceneManager.LoadScene("GameLevel1");
    }

    private void OpenGlobalShop()
    {
        _gameOverWindow.SetActive(false);
        _globalShop.gameObject.SetActive(true);
        _globalShop.CloseButton.onClick.AddListener(ShowGameOverMenu);
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

    public void ShowGameOverMenu()
    {
        GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = true;
        _gameOverWindow.SetActive(true);
        TrySetBuyContinueButtonInteractable();
        _globalShop.CloseButton.onClick.RemoveListener(ShowGameOverMenu);
    }

    private void TrySetBuyContinueButtonInteractable()
    {
        _buyContinueGameButton.SetInteractable(GameManager.Instance.CurrentGameData.CountResetLevelCoins > 0);
        _buyContinueGameButton.CountText.text =
            GameManager.Instance.CurrentGameData.CountResetLevelCoins.ToString();

        _resetLevelButton.interactable = GameManager.Instance.CurrentGameData.ResetLevelBought;
    }

    public void BuyResetLevel()
    {
        if(_resetLevelButton.interactable)
            return;

        _resetLevelButton.interactable = true;
        GameManager.Instance.CurrentGameData.ResetLevelBought = true;
        GameManager.Instance.CurrentGameData.CountResetLevelCoins--;
        _buyContinueGameButton.CountText.text = 
            GameManager.Instance.CurrentGameData.CountResetLevelCoins.ToString();
        
        if(GameManager.Instance.CurrentGameData.CountResetLevelCoins == 0)
            _buyContinueGameButton.SetInteractable(false);
        SaveSystem.SaveSystem.SaveGame();
    }

    private void OnEnable()
    {
        TrySetBuyContinueButtonInteractable();
        _buyContinueGameButton.Button.onClick.AddListener(BuyResetLevel);
        _resetLevelButton.onClick.AddListener(ResetLevel);
        _resetGameButton.onClick.AddListener(ResetGame);
        _globalShopButton.onClick.AddListener(OpenGlobalShop);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        _buyContinueGameButton.Button.onClick.RemoveListener(BuyResetLevel);
        _resetLevelButton.onClick.RemoveListener(ResetLevel);
        _resetGameButton.onClick.RemoveListener(ResetGame);
        _globalShopButton.onClick.RemoveListener(OpenGlobalShop);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
        _quitButton.onClick.RemoveListener(Quit);
    }

}

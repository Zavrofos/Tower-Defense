using System;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using GameOverlayWindow;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitButton;

    [SerializeField] private GameObject ItemsParent;
    [SerializeField] private WinMenuNewItem _winMenuNewItem1;
    [SerializeField] private WinMenuNewItem _winMenuNewItem2;
    
    private List<GlobalShopItemType> _rewardItemsToShow = new ();

    private void Continue()
    {
        GameManager.Instance.ObjectPooler.ClearPool();
        SceneManager.UnloadSceneAsync($"GameLevel_{GameManager.Instance.CurrentWorld}_{GameManager.Instance.CurrentLevel}");
        GameManager.Instance.GameHub.gameObject.SetActive(true);
        GameManager.Instance.GameOverlay.gameObject.SetActive(false);
        GameManager.Instance.SetNormalSpeedGame();
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        _continueButton.onClick.AddListener(Continue);
        _mainMenuButton.onClick.AddListener(MainMenu);
        _quitButton.onClick.AddListener(Quit);
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

    public void SetRewardItemsToShow(List<GlobalShopItemType> rewards)
    {
        _rewardItemsToShow = new List<GlobalShopItemType>(rewards);
    }

    private void OnEnable()
    {
        if(_rewardItemsToShow.Count == 0)
            return;
        
        _winMenuNewItem1.Setup(_rewardItemsToShow[0]);
        _winMenuNewItem1.gameObject.SetActive(true);

        if (_rewardItemsToShow.Count > 1)
        {
            _winMenuNewItem2.Setup(_rewardItemsToShow[1]);
            _winMenuNewItem2.gameObject.SetActive(true);
        }
        
        ItemsParent.SetActive(true);
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }
}

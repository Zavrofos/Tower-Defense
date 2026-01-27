using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GlobalShop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerInGame : MonoBehaviour
{
    [SerializeField] private Button PauseButton;
    [SerializeField] private Transform _conteiner;
    [SerializeField] private PouseMenu _pouseMenu;
    [SerializeField] private TMP_Text _countCoins;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private GameObject WinWindow;
    [SerializeField] private GameObject WinGameWindow;
    [SerializeField] private Home _home;
    [SerializeField] private int _coins = 20;
    [SerializeField] private int RevardForWinLevel = 100;
    [SerializeField] private int RevardGameOverLevel = 50;
    public Button SetNextWaveButton;
    public Button SetGameFasterButton;
    private int _mineCost = 10;
    public bool IsPouse;
    private bool _isGameOver;
    public Shop[] Shops;

    public ButtonAbility RokketButtonAbility;
    public ButtonAbility MineButtonAbility;

    private Spawner _spawner;

    public bool IsDisableButtonColliders = false;

    public int Coins => _coins;
    public int MineCost => _mineCost;
    
    public bool FastGameEnabled { get; private set; } 

    private void Awake()
    {
        CheckBoughtAbilityAndTrySetActive();
        GameManager.Instance.CurrentGameManagerLevel = this;
    }

    private void Start()
    {
        _countCoins.text = _coins.ToString();
        _spawner = GameManager.Instance.CurrentSpawner;
        PauseButton.onClick.AddListener(() => PauseGame(true));
        SetNextWaveButton.onClick.AddListener(SetNextWave);
        SetGameFasterButton.onClick.AddListener(SwitchGameFaster);
        GameManager.Instance.CurrentSpawner.OnSetNextWave += SetInteractableNextWaveButton;
    }

    private void Update()
    {
        bool winWindowOpened = WinWindow && WinWindow.gameObject.activeSelf || WinGameWindow && WinGameWindow.activeSelf;
        
        if (_spawner.IsWin && !winWindowOpened)
            ShowWinWindow();
        
        if (_spawner.IsWin || _isGameOver) 
            return;
        
        if(Input.GetKeyDown(KeyCode.Escape) && !_pouseMenu.IsPouse)
            PauseGame(true);
        else if(Input.GetKeyDown(KeyCode.Escape) && _pouseMenu.IsPouse)
            PauseGame(false);
    }

    private void SetNextWave()
    {
        Spawner currenSpawner = GameManager.Instance.CurrentSpawner;
        currenSpawner.SetNextWave();
    }
    
    private void SwitchGameFaster()
    {
        FastGameEnabled = !FastGameEnabled;
        Time.timeScale = FastGameEnabled ? 2f : 1f;
        GameManager.Instance.CurrentSpeedGame = Time.timeScale;
    }

    private void SetInteractableNextWaveButton(bool value)
    {
        SetNextWaveButton.interactable = value;
    }
    
    private void CheckBoughtAbilityAndTrySetActive()
    {
        RokketButtonAbility.gameObject.SetActive(GameManager.Instance.CurrentGameData.IsRocketAbilityBought);
        MineButtonAbility.gameObject.SetActive(GameManager.Instance.CurrentGameData.IsMineAbilityBought);
        MineButtonAbility.SetInteractableButton(GameManager.Instance.CurrentGameData.CountMineBought > 0);
        MineButtonAbility.SetCountMine(GameManager.Instance.CurrentGameData.CountMineBought);
    }

    private void ShowWinWindow()
    {
        GameManager.Instance.SetNormalSpeedGame();
        
        CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
        
        if (currentGameData.CurrentLevel == GameManager.Instance.CountLevels)
        {
            currentGameData.IsWinGame = true;
            currentGameData.IsWinLevel = false;
            WinGameWindow.SetActive(true);
        }
        else
        {
            currentGameData.IsWinLevel = true;
            currentGameData.CurrentLevel++;
            currentGameData.CurrentGlobalMoney += RevardForWinLevel;
            WinWindow.SetActive(true);
        }

        IsDisableButtonColliders = true;
        
        SaveSystem.SaveSystem.SaveGame();
    }

    private void PauseGame(bool isPause)
    {
        Time.timeScale = isPause ? 0 : GameManager.Instance.CurrentSpeedGame;
        _pouseMenu.gameObject.SetActive(isPause);
        IsDisableButtonColliders = isPause;
        _pouseMenu.IsPouse = isPause;
        IsPouse = isPause;
        
        if(isPause)
            AudioManager.Instance.PauseAudio();
        else
            AudioManager.Instance.PlayAudio();
    }
     
    public void OpenShop(Shop shop)
    {
        foreach(var item in Shops)
        {
            if (item == shop) continue;
            item.Close();
        }
    }

    public void AddCoins(int coins)
    {
        _coins += coins;
        _countCoins.text = _coins.ToString();
    }

    public void SpendCoins(int coins)
    {
        _coins -= coins;
        _countCoins.text = _coins.ToString();
    }

    public void GameOverLevel()
    {
        GameManager.Instance.CurrentGameData.IsGameOverLevel = true;
        GameManager.Instance.CurrentGameData.IsWinLevel = false;
        GameManager.Instance.CurrentGameData.CurrentGlobalMoney += RevardGameOverLevel;
        GameManager.Instance.SetNormalSpeedGame();
        SaveSystem.SaveSystem.SaveGame();
        GameOverWindow.SetActive(true);
        AudioManager.Instance.PauseAudio();
        IsPouse = true;
        IsDisableButtonColliders = true;
        _isGameOver = true;
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _home.Killed += GameOverLevel;
    }

    private void OnDisable()
    {
        _home.Killed -= GameOverLevel;
    }

    private void OnDestroy()
    {
        PauseButton.onClick.RemoveAllListeners();
        SetNextWaveButton.onClick.RemoveAllListeners();
        SetGameFasterButton.onClick.RemoveAllListeners();
    }
}

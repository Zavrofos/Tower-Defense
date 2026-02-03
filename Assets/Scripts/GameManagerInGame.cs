using System;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.Scripts.GlobalShop;
using Assets.Scripts.MeteorsAbility;
using Assets.Scripts.RepPoolObject;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public bool IsPouse;
    private bool _isGameOver;
    public Shop[] Shops;
    public Transform[] PointsOfWayForEnemy;
    public Transform[] PointsForMeteors;

    public HealthBar HealthBar;
    public ButtonAbility RokketButtonAbility;
    public ButtonAbility MineButtonAbility;
    public ButtonAbilityUI FoodAbility;
    public ButtonAbilityUI PocketMoneyAbility;
    public ButtonAbilityUI MeteorShowerAbilityButton;

    private Spawner _spawner;

    public bool IsDisableButtonColliders = false;

    public int Coins => _coins;
    
    public bool FastGameEnabled { get; private set; }

    public List<Enemy> CurrentEnemies { get; private set; } = new();
    public List<AbsTower> CurrentTowers { get; private set; } = new();

    private CancellationTokenSource _cancellationTokenSourceMeteors = new();

    private void Awake()
    {
        CheckBoughtAbilityAndTrySetActive();
        InitButtonAbilityUI();
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
        
        FoodAbility.gameObject.SetActive(GameManager.Instance.CurrentGameData.FoodAbilityBought);
        FoodAbility.SetInteractableButton(GameManager.Instance.CurrentGameData.CountFoodBought > 0);
        FoodAbility.SetCount(GameManager.Instance.CurrentGameData.CountFoodBought);
        
        PocketMoneyAbility.gameObject.SetActive(GameManager.Instance.CurrentGameData.MoneyPocketAbilityBought);
        PocketMoneyAbility.SetInteractableButton(GameManager.Instance.CurrentGameData.CountMoneyPocketsBought > 0);
        PocketMoneyAbility.SetCount(GameManager.Instance.CurrentGameData.CountMoneyPocketsBought);
        
        MeteorShowerAbilityButton.gameObject.SetActive(GameManager.Instance.CurrentGameData.MeteorShowerBought);
        MeteorShowerAbilityButton.SetInteractableButton(GameManager.Instance.CurrentGameData.CountMeteorShowerBought > 0);
        MeteorShowerAbilityButton.SetCount(GameManager.Instance.CurrentGameData.CountMeteorShowerBought);
    }

    private void InitButtonAbilityUI()
    {
        FoodAbility.Button.onClick.AddListener(() =>
        {
            CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
            HealthBar.Home.AddHealth(20);
            currentGameData.CountFoodBought--;
            FoodAbility.Count.text = currentGameData.CountFoodBought.ToString();
            if(currentGameData.CountFoodBought == 0)
                FoodAbility.SetInteractableButton(false);
        });
        
        PocketMoneyAbility.Button.onClick.AddListener(() =>
        {
            CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
            _coins += 10;
            _countCoins.text = _coins.ToString();
            currentGameData.CountMoneyPocketsBought--;
            PocketMoneyAbility.Count.text = currentGameData.CountMoneyPocketsBought.ToString();
            if(currentGameData.CountMoneyPocketsBought == 0)
                PocketMoneyAbility.SetInteractableButton(false);
        });
        
        MeteorShowerAbilityButton.Button.onClick.AddListener(() =>
        {
            CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
            currentGameData.CountMeteorShowerBought--;
            MeteorShowerAbilityButton.Count.text = currentGameData.CountMeteorShowerBought.ToString();
            if(currentGameData.CountMeteorShowerBought == 0)
                MeteorShowerAbilityButton.SetInteractableButton(false);
            
            PlayMeteorShower().Forget();
        });
    }
    
    private async UniTask PlayMeteorShower()
    {
        _cancellationTokenSourceMeteors?.Cancel();
        _cancellationTokenSourceMeteors?.Dispose();
        _cancellationTokenSourceMeteors = new CancellationTokenSource();
        
        CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
        MeteorShowerAbilityButton.SetInteractableButton(false);

        var tasks = new UniTask[PointsForMeteors.Length];

        for (int i = 0; i < PointsForMeteors.Length; i++)
        {
            Vector3 pos = PointsForMeteors[i].position;
            tasks[i] = SpawnMeteorsAtPoint(pos, _cancellationTokenSourceMeteors.Token);
        }

        await UniTask.WhenAll(tasks);
        
        if(_cancellationTokenSourceMeteors.Token.IsCancellationRequested)
            return;
        
        MeteorShowerAbilityButton.SetInteractableButton(currentGameData.CountMeteorShowerBought > 0);
    }
    
    private async UniTask SpawnMeteorsAtPoint(Vector3 position, CancellationToken token)
    {
        const int meteorsCount = 3;

        for (int i = 0; i < meteorsCount; i++)
        {
            float delay = Random.Range(1f, 3f);
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            
            if(token.IsCancellationRequested)
                return;

            Meteor meteor = (Meteor)ObjectPooler.Instance
                .SpawnFromPool("Meteor", position, Quaternion.identity);

            meteor.PlayMeteorsShower(token).Forget();
        }
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
        FoodAbility.Button.onClick.RemoveAllListeners();
        PocketMoneyAbility.Button.onClick.RemoveAllListeners();
        MeteorShowerAbilityButton.Button.onClick.RemoveAllListeners();
        _cancellationTokenSourceMeteors?.Cancel();
        _cancellationTokenSourceMeteors?.Dispose();
    }
}

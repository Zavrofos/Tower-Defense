using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManagerInGame : MonoBehaviour
{
    [SerializeField] private Transform _conteiner;
    [SerializeField] private PouseMenu _pouseMenu;
    [SerializeField] private TMP_Text _countCoins;
    [SerializeField] private GameObject GameOverWindow;
    [SerializeField] private Home _home;
    [SerializeField] private LevelView _levelViewPrefab;
    private int _mineCost = 10;
    private int _coins = 100;
    public bool IsPouse;
    private bool _isGameOver;
    public Shop[] Shops;

    public ButtonAbility RokketButtonAbility;
    public ButtonAbility MineButtonAbility;

    private Spawner _spawner;

    public bool IsDisableButtonColliders = false;

    public int Coins => _coins;
    public int MineCost => _mineCost;
    private AudioManager _audioManager;

    private void Start()
    {
        _countCoins.text = _coins.ToString();
        LevelsManager levelsManager = FindObjectOfType<LevelsManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        if(levelsManager != null)
        {
            for(int i = 0; i < levelsManager.Levels.Count; i++)
            {
                Level level = levelsManager.Levels[i];
                LevelView levelPref = Instantiate(_levelViewPrefab, _conteiner);
                levelPref.Level = level;
                levelPref.LabelText.text = level.Label;
                if (level.IsOpen) levelPref.OpenLevel();
            }
        }
        _spawner = FindObjectOfType<Spawner>();
    }

    private void Update()
    {
        if (_spawner.IsWin || _isGameOver) return;
        if(Input.GetKeyDown(KeyCode.Escape) && !_pouseMenu.IsPouse)
        {
            Time.timeScale = 0;
            _pouseMenu.gameObject.SetActive(true);
            IsDisableButtonColliders = true;
            _pouseMenu.IsPouse = true;
            IsPouse = true;
            _audioManager.PauseAudio();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _pouseMenu.IsPouse)
        {
            Time.timeScale = 1;
            _pouseMenu.gameObject.SetActive(false);
            IsDisableButtonColliders = false;
            _pouseMenu.IsPouse = false;
            IsPouse = false;
            _audioManager.PlayAudio();
        }
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

    public void GameOver()
    {
        GameOverWindow.SetActive(true);
        _audioManager.PauseAudio();
        IsPouse = true;
        IsDisableButtonColliders = true;
        _isGameOver = true;
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _home.Killed += GameOver;
    }

    private void OnDisable()
    {
        _home.Killed -= GameOver;
    }
}

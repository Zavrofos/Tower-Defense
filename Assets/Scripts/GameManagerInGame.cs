using Assets.Scripts;
using System.Collections.Generic;
using Assets.Scripts.GlobalShop;
using SaveSystemDir;
using UnityEngine;

public class GameManagerInGame : MonoBehaviour
{
    [field: SerializeField] public float CameraSize { get; private set; }
    [field: SerializeField] public Vector3 CameraPos { get; private set; }
    [field: SerializeField] public Canvas Canvas { get; private set; }
    [field: SerializeField] public Home Home { get; private set; }
    
    [SerializeField] private int RevardForWinLevel = 100;
    [SerializeField] private int RevardGameOverLevel = 50;
    [SerializeField] private List<GlobalShopItemType> RewardItems = new ();

    public Shop[] Shops;
    public Transform[] PointsOfWayForEnemy;
    public Transform[] PointsForMeteors;
    public bool IsDisableButtonColliders { get; set; }
    public List<Enemy> CurrentEnemies { get; private set; } = new();
    public List<AbsTower> CurrentTowers { get; private set; } = new();
    
    private void Awake()
    {
        GameManager.Instance.CurrentGameManagerLevel = this;
        Camera.main.gameObject.transform.position = CameraPos;
        Camera.main.orthographicSize = CameraSize;
        Canvas.worldCamera = Camera.main;
    }

    private void Start()
    {
        GameManager.Instance.CurrentSpawner.OnWinLevel += ShowWinWindow;
        GameManager.Instance.CurrentSpawner.OnWinLevel += () => GameManager.Instance.PauseGame(true, false);
        GameManager.Instance.GameOverlay.CoinsText.text = "100";
    }
    
    private void ShowWinWindow()
    {
        OpenNextLevel();
        SaveSystem.CurrentGameData.CurrentGlobalMoney += RevardForWinLevel;

        foreach (var rewardItem in RewardItems)
        {
            if (SaveSystem.CurrentGameData.TowersData.TryGetValue(rewardItem, out var value))
                value.IsOpenToBuy = true;
            
            if (SaveSystem.CurrentGameData.AbilityData.TryGetValue(rewardItem, out var value1))
                value1.IsOpenToBuy = true;
        }
        
        SaveSystem.SaveGame();
        
        GameManager.Instance.SetNormalSpeedGame();
        GameManager.Instance.WinMenu.SetRewardItemsToShow(RewardItems);
        GameManager.Instance.WinMenu.gameObject.SetActive(true);
        IsDisableButtonColliders = true;
    }

    private void OpenNextLevel()
    {
        if(GameManager.Instance.CurrentWorld == 3 && GameManager.Instance.CurrentLevel == 4)
            return;

        int nextLevel = GameManager.Instance.CurrentLevel < 4
            ? GameManager.Instance.CurrentLevel + 1
            : 1;
        
        int nextWorld = GameManager.Instance.CurrentWorld < 3 && nextLevel == 1
            ? GameManager.Instance.CurrentWorld + 1
            : GameManager.Instance.CurrentWorld;

        SaveSystem.CurrentGameData.Worlds[nextWorld - 1] = 1;
        SaveSystem.CurrentGameData.Levels[nextWorld - 1].Cols[nextLevel - 1] = 1;
    }
     
    public void OpenShop(Shop shop)
    {
        foreach(var item in Shops)
        {
            if (item == shop) continue;
            item.Close();
        }
    }

    private void GameOverLevel()
    {
        SaveSystem.CurrentGameData.CurrentGlobalMoney += RevardGameOverLevel;
        GameManager.Instance.SetNormalSpeedGame();
        SaveSystem.SaveGame();
        GameManager.Instance.GameOverMenu.gameObject.SetActive(true);
        IsDisableButtonColliders = true;
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        Home.Killed += GameOverLevel;
    }

    private void OnDisable()
    {
        Home.Killed -= GameOverLevel;
    }
}

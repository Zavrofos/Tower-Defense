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
        Debug.Log("(test) ShowWinWindow");
        GameManager.Instance.SetNormalSpeedGame();
        CurrentGameData currentGameData = SaveSystem.CurrentGameData;
        GameManager.Instance.WinMenu.gameObject.SetActive(true);
        IsDisableButtonColliders = true;
        currentGameData.Worlds[GameManager.Instance.CurrentWorld - 1] = 1;
        currentGameData.Levels[GameManager.Instance.CurrentWorld - 1, GameManager.Instance.CurrentLevel - 1] = 1;
        currentGameData.CurrentGlobalMoney += RevardForWinLevel;
        SaveSystem.SaveGame();
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

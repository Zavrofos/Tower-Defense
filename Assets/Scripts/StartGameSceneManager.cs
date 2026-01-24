using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using UnityEngine;

public class StartGameSceneManager : MonoBehaviour
{
    [field: SerializeField] public GameObject WinLevelMenu { get; private set; }
    [field: SerializeField] public GameObject WinGameMenu { get; private set; }
    [field: SerializeField] public GameObject GameOverMenu { get; private set; }

    private void Awake()
    {
        CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
            
        if(currentGameData.IsWinGame)
            WinGameMenu.SetActive(true);
            
        if(currentGameData.IsWinLevel)
            WinLevelMenu.SetActive(true);
            
        if(currentGameData.IsGameOverLevel)
            GameOverMenu.SetActive(true);
    }
}
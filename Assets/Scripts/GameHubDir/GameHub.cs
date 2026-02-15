using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using SaveSystemDir;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameHubDir
{
    public class GameHub : MonoBehaviour
    {
        [field: SerializeField] public Button PlayButton { get; private set; }
        [field: SerializeField] public Button MenuButton { get; private set; }
        [field: SerializeField] public Button ShopButton { get; private set; }
        [field: SerializeField] public GlobalShop ShopWindow { get; private set; }
        [field: SerializeField] public List<WorldPanel> WorldsPanels { get; private set; }

        private void UpdateHub()
        {
            CurrentGameData currentGameData = SaveSystem.CurrentGameData;

            for (int i = 0; i < WorldsPanels.Count; i++)
            {
                WorldsPanels[i].SetInteractable(currentGameData.Worlds[i] == 1);
                
                for (int j = 0; j < WorldsPanels[i].LevelsButtons.Count; j++)
                    WorldsPanels[i].LevelsButtons[j].SetInteractable(currentGameData.Levels[i].Cols[j] == 1);
            }
        }

        private void Awake()
        {
            foreach (var worldPanel in WorldsPanels)
                foreach (var levelButton in worldPanel.LevelsButtons)
                    levelButton.Button.onClick
                        .AddListener(() => LoadLevel(levelButton.WorldNumber, levelButton.LevelNumber));
            
            PlayButton.onClick.AddListener(PlayGame);
            MenuButton.onClick.AddListener(MainMenu);
            ShopButton.onClick.AddListener(OpenShop);
        }

        private void OnEnable()
        {
            UpdateHub();
        }

        private void OnDestroy()
        {
            foreach (var worldPanel in WorldsPanels)
                foreach (var levelButton in worldPanel.LevelsButtons)
                    levelButton.Button.onClick.RemoveAllListeners();
            
            PlayButton.onClick.RemoveAllListeners();
            MenuButton.onClick.RemoveAllListeners();
            ShopButton.onClick.RemoveAllListeners();
        }

        private void LoadLevel(int world, int level)
        {
            string levelName = $"GameLevel_{world}_{level}";
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
            GameManager.Instance.CurrentWorld = world;
            GameManager.Instance.CurrentLevel = level;
            GameManager.Instance.GameHub.gameObject.SetActive(false);
            GameManager.Instance.GameOverlay.gameObject.SetActive(true);
        }

        private void PlayGame()
        {
            CurrentGameData currentGameData = SaveSystem.CurrentGameData;

            int lengthRow = currentGameData.Levels.GetLength(0);
            int lengthColumn = currentGameData.Levels.GetLength(1);
            
            for (int i = lengthRow - 1; i >= 0; i--)
            {
                for (int j = lengthColumn - 1; j >= 0; j--)
                {
                    bool completed = currentGameData.Levels[i].Cols[j] == 1;

                    if (!completed) 
                        continue;
                    
                    LoadLevel(i + 1, j + 1);
                    return;
                }
            }
        }

        private void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OpenShop()
        {
            ShopWindow.gameObject.SetActive(true);
        }
    }
}
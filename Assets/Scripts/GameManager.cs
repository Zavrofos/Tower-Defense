using Assets.Scripts.RepPoolObject;
using GameHubDir;
using GameOverlayWindow;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [field: SerializeField] public ObjectPooler ObjectPooler { get; set; }
        [field: SerializeField] public GameAssets GameAssets { get; set; }
        [field: SerializeField] public GameHub GameHub { get; set; }
        [field: SerializeField] public SettingsMenu SettingsMenu { get; set; }
        [field: SerializeField] public WinMenu WinMenu { get; set; }
        [field: SerializeField] public GameOver GameOverMenu { get; set; }
        [field: SerializeField] public GameOverlay GameOverlay { get; set; }
        [field: SerializeField] public LoadingWindow LoadingWindow { get; set; }
        [field: SerializeField] public PauseMenu PauseMenu { get; set; }
        
        public int CurrentWorld { get; set; }
        public int CurrentLevel { get; set; }
        public GameManagerInGame CurrentGameManagerLevel { get; set; }
        public Spawner CurrentSpawner { get; set; }
        public float CurrentSpeedGame { get; set; } = 1;
        public bool FastGameEnabled { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            GameOverlay.PauseGameButton.onClick.AddListener(() => PauseGame(true));
            GameOverlay.SetGameFasterButton.onClick.AddListener(SwitchGameFaster);
        }
        
        public void SetNormalSpeedGame()
        {
            Time.timeScale = 1f;
            CurrentSpeedGame = 1;
        }
        
        public void PauseGame(bool isPause, bool showPauseWindow = true)
        {
            Time.timeScale = isPause ? 0 : CurrentSpeedGame;
            PauseMenu.gameObject.SetActive(isPause && showPauseWindow);
            CurrentGameManagerLevel.IsDisableButtonColliders = isPause;
            PauseMenu.IsPause = isPause;
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.IsPause)
            {
                PauseGame(true);
                PauseMenu.IsPause = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && PauseMenu.IsPause)
            {
                PauseGame(false, false);
                PauseMenu.IsPause = false;
            }
        }
        
        private void SwitchGameFaster()
        {
            FastGameEnabled = !FastGameEnabled;
            Time.timeScale = FastGameEnabled ? 2f : 1f;
            CurrentSpeedGame = Time.timeScale;
        }

        private void OnDestroy()
        {
            GameOverlay.PauseGameButton.onClick.RemoveAllListeners();
            GameOverlay.SetGameFasterButton.onClick.RemoveAllListeners();
        }
    }
}
using System.IO;
using Assets.Scripts.GlobalShop;
using UnityEngine;

namespace SaveSystemDir
{
    public static class SaveSystem
    {
        public static CurrentGameData CurrentGameData { get; private set; }
        
        private const string SaveQualityPrefKey = "SaveQualityPrefKey";
        private const string SaveResolutionsPrefKey = "SaveResolutionsPrefKey";
        private const string SaveFullScreenPrefKey = "SaveFullScreenPrefKey";
        private const string SaveVolumeMusicPrefKey = "SaveVolumeMusicPrefKey";
        private const string SaveVolumeGamePrefKey = "SaveVolumeGamePrefKey";
        
        private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoInitialize()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (CurrentGameData != null) return; // защита от двойного init

            CurrentGameData = LoadSaveGameData();
            CurrentGameData.Init();
        }
        
        public static void SaveGame()
        {
            try
            {
                string json = JsonUtility.ToJson(CurrentGameData, true);
                File.WriteAllText(SavePath, json);
                Debug.Log($"[SaveSystem] Saved: {SavePath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Save error: {e}");
            }
        }

        public static CurrentGameData LoadSaveGameData()
        {
            try
            {
                if (!File.Exists(SavePath))
                {
                    Debug.Log("[SaveSystem] No save file, creating default data");
                    return new CurrentGameData();
                }

                string json = File.ReadAllText(SavePath);
                var data = JsonUtility.FromJson<CurrentGameData>(json);

                if (data == null)
                {
                    Debug.LogWarning("[SaveSystem] Save file broken, creating default data");
                    return new CurrentGameData();
                }

                Debug.Log($"[SaveSystem] Loaded: {SavePath}");
                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Load error: {e}");
                return new CurrentGameData();
            }
        }

        public static void SaveQuality(int quality)
        {
            PlayerPrefs.SetInt(SaveQualityPrefKey, (int)quality);
        }

        public static int GetQuality()
        {
            if (!PlayerPrefs.HasKey(SaveQualityPrefKey))
                return 1;
            
            return PlayerPrefs.GetInt(SaveQualityPrefKey, 0);
        }
        
        public static void SaveResolutions(int resolutions)
        {
            PlayerPrefs.SetInt(SaveResolutionsPrefKey, (int)resolutions);
        }

        public static int GetResolutions()
        {
            if (!PlayerPrefs.HasKey(SaveResolutionsPrefKey))
                return -1;
            
            return PlayerPrefs.GetInt(SaveResolutionsPrefKey, 0);
        }
        
        public static void SaveFullScreen(bool value)
        {
            PlayerPrefs.SetInt(SaveFullScreenPrefKey, value ? 1 : 0);
        }

        public static bool GetFullScreen()
        {
            if (!PlayerPrefs.HasKey(SaveFullScreenPrefKey))
                return true;
            
            return PlayerPrefs.GetInt(SaveFullScreenPrefKey, 0) == 1;
        }
        
        public static void SaveVolumeMusicScreen(float value)
        {
            PlayerPrefs.SetFloat(SaveVolumeMusicPrefKey, value);
        }

        public static float GetVolumeMusic()
        {
            if (!PlayerPrefs.HasKey(SaveVolumeMusicPrefKey))
                return 0.5f;
            
            return PlayerPrefs.GetFloat(SaveVolumeMusicPrefKey, 0);
        }
        
        public static void SaveVolumeGameScreen(float value)
        {
            PlayerPrefs.SetFloat(SaveVolumeGamePrefKey, value);
        }

        public static float GetVolumeGame()
        {
            if (!PlayerPrefs.HasKey(SaveVolumeGamePrefKey))
                return 0.5f;
            
            return PlayerPrefs.GetFloat(SaveVolumeGamePrefKey, 0);
        }
    }
}
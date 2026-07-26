using System.IO;
using Assets.Scripts.GlobalShop;
using UnityEngine;

namespace SaveSystemDir
{
    public static class SaveSystem
    {
        public static CurrentGameData CurrentGameData { get; private set; }
        
        private const string SaveVolumeMusicPrefKey = "SaveVolumeMusicPrefKey";
        private const string SaveVolumeGamePrefKey = "SaveVolumeGamePrefKey";
        private const string SaveLocalizationPrefKey = "SaveLocalizationPrefKey";
        
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
        
        public static void SaveVolumeMusicScreen(float value)
        {
            PlayerPrefs.SetFloat(SaveVolumeMusicPrefKey, value);
            PlayerPrefs.Save();
        }

        public static void SaveLocalization(Language language)
        {
            PlayerPrefs.SetInt(SaveLocalizationPrefKey, (int)language);
            PlayerPrefs.Save();
        }

        public static Language GetCurrentLanguage()
        {
            // язык по умолчанию (до первого выбора игроком) — английский
            return (Language) PlayerPrefs.GetInt(SaveLocalizationPrefKey, (int)Language.English);
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
            PlayerPrefs.Save();
        }

        public static float GetVolumeGame()
        {
            if (!PlayerPrefs.HasKey(SaveVolumeGamePrefKey))
                return 0.5f;
            
            return PlayerPrefs.GetFloat(SaveVolumeGamePrefKey, 0);
        }
    }
}
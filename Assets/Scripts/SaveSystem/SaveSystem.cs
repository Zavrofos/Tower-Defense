using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveSystem
    {
        private const string LevelPrefKey = "SaveLevelPrefKey";
        private const string SaveQualityPrefKey = "SaveQualityPrefKey";
        private const string SaveResolutionsPrefKey = "SaveResolutionsPrefKey";
        private const string SaveFullScreenPrefKey = "SaveFullScreenPrefKey";
        private const string SaveVolumeMusicPrefKey = "SaveVolumeMusicPrefKey";
        private const string SaveVolumeGamePrefKey = "SaveVolumeGamePrefKey";
        
        public static void SaveLevels(List<Level> levels)
        {
            foreach (Level level in levels)
            {
                if(level == null)
                    return;
                
                PlayerPrefs.SetInt($"{LevelPrefKey}{level.Label}", level.IsOpen ? 1 : 0);
            }
        }

        public static LevelsData LoadLevels(int countLevels)
        {
            List<Level> levels = new List<Level>();

            for (int i = 0; i < countLevels; i++)
            {
                int levelNumber = i + 1;
                bool isOpen = PlayerPrefs.GetInt($"{LevelPrefKey}{levelNumber.ToString()}") == 1;
                isOpen = levelNumber == 1 || isOpen;
                Level level = new Level(levelNumber);
                level.IsOpen = isOpen;
                levels.Add(level);
            }

            return new LevelsData(levels);
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
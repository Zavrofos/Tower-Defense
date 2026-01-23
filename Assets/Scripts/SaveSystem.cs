using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts
{
    public static class SaveSystem
    {
        private const string LevelPrefKey = "SaveLevelPrefKey";
        
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
    }
}
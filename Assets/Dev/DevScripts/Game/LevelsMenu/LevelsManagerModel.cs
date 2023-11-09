using System;
using System.Collections.Generic;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class LevelsManagerModel 
    {
        public readonly Dictionary<string, LevelModel> Levels = new();
        public event Action OpenedLevelsMenu;
        public event Action ClosedLevelsMenu;
        public event Action<LevelModel> AddedLevel;

        public void OpenLevelsMenu()
        {
            OpenedLevelsMenu?.Invoke();
        }

        public void CloseLevelsMenu()
        {
            ClosedLevelsMenu?.Invoke();
        }

        public void AddLevel(LevelModel level)
        {
            AddedLevel?.Invoke(level);
        }
    }
}
using System;
using System.Collections.Generic;
using Assets.Dev.DevScripts.Main_Menu.LevelsMenu;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class LevelsManagerModel 
    {
        public List<LevelDev> Levels;
        public event Action OpenedLevelsMenu;
        public event Action ClosedLevelsMenu;

        public LevelsManagerModel()
        {
            Levels = new List<LevelDev>();
        }
        
        public void OpenLevelsMenu()
        {
            OpenedLevelsMenu?.Invoke();
        }

        public void CloseLevelsMenu()
        {
            ClosedLevelsMenu?.Invoke();
        }
    }
}
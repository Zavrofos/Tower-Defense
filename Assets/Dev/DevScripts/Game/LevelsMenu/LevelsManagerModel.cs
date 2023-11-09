using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu.LevelsMenu
{
    public class LevelsManagerModel 
    {
        public List<LevelDev> Levels;
        public event Action OpenedLevelsMenu;
        public event Action ClosedLevelsMenu;

        public LevelsManagerModel()
        {
            Levels = new();
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
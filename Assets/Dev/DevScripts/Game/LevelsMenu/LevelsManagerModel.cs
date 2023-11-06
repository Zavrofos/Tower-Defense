using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu.LevelsMenu
{
    public class LevelsManagerModel 
    {
        public List<LevelDev> Levels;

        public LevelsManagerModel()
        {
            Levels = new();
        }
    }
}
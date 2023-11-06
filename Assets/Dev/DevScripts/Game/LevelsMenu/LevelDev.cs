using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu.LevelsMenu
{
    public class LevelDev 
    {
        public string NumberLevel;
        public bool IsBlock;

        public LevelDev(string numberLevel)
        {
            NumberLevel = numberLevel;
        }
    }
}
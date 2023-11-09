﻿using System;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class LevelModel 
    {
        public string NumberLevel;
        public bool IsBlock;
        public event Action OpenedLevel;

        public LevelModel(string numberLevel)
        {
            NumberLevel = numberLevel;
        }

        public void OpenLevel()
        {
            OpenedLevel?.Invoke();
        }
    }
}
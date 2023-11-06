using Assets.Dev.DevScripts.Main_Menu.LevelsMenu;
using System;
using UnityEngine;

namespace Assets.Dev.DevScripts
{
    public class GameModel 
    {
        public LevelsManagerModel LevelsManager;
        public event Action Initialized;
        public GameModel()
        {
            LevelsManager = new();
        }

        public void Initialize()
        {
            Initialized?.Invoke();
        }
    }
}

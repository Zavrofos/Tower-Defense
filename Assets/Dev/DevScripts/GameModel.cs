using Assets.Dev.DevScripts.Game.OptionsMenu;
using Assets.Dev.DevScripts.Main_Menu.LevelsMenu;
using System;
using UnityEngine;

namespace Assets.Dev.DevScripts
{
    public class GameModel 
    {
        public LevelsManagerModel LevelsManager;
        public SettingsModel SettingsModel;
        public event Action Initialized;
        public GameModel()
        {
            LevelsManager = new();
            SettingsModel = new();
        }

        public void Initialize()
        {
            Initialized?.Invoke();
        }
    }
}

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

        public StateGame CurrentStateGame;

        public event Action Initialized;
        public event Action PressedPause;
        
        public GameModel()
        {
            LevelsManager = new();
            SettingsModel = new();
            CurrentStateGame = StateGame.InGame;
        }

        public void Initialize()
        {
            Initialized?.Invoke();
        }

        public void PressPause()
        {
            PressedPause?.Invoke();
        }
    }
}

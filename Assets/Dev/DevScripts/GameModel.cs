using Assets.Dev.DevScripts.Game.OptionsMenu;
using Assets.Dev.DevScripts.Game.PauseMenu;
using System;
using Dev.DevScripts.Game.LevelsMenu;
using UnityEngine;

namespace Assets.Dev.DevScripts
{
    public class GameModel 
    {
        public LevelsManagerModel LevelsManager;
        public SettingsModel SettingsModel;
        public PauseModel PauseModel;

        public StateGame CurrentStateGame;
        public int CurrentLevel;

        public event Action Initialized;
        public event Action PressedEscape;
        
        public GameModel()
        {
            LevelsManager = new();
            SettingsModel = new();
            PauseModel = new();
            CurrentStateGame = StateGame.InGame;
        }

        public void Initialize()
        {
            Initialized?.Invoke();
        }

        public void PressEscape()
        {
            PressedEscape?.Invoke();
        }
    }
}

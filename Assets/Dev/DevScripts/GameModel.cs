using System;
using Dev.DevScripts.Game.LevelsMenu;
using Dev.DevScripts.Game.PauseMenu;
using Dev.DevScripts.Game.SettingsMenu;

namespace Dev.DevScripts
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

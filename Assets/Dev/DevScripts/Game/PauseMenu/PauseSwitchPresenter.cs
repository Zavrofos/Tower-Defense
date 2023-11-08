using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.Pause
{
    public class PauseSwitchPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public PauseSwitchPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.PressedEscape += OnTurnPause;
        }

        public void Unsubscribe()
        {
            _model.PressedEscape -= OnTurnPause;
        }

        private void OnTurnPause()
        {
            if(_model.CurrentStateGame == StateGame.InGame)
            {
                _view.PauseMenuView.gameObject.SetActive(true);
                Time.timeScale = 0;
                _model.CurrentStateGame = StateGame.OnPause;
            }
            else if(_model.CurrentStateGame == StateGame.OnPause 
                && !_view.LevelsMenuView.gameObject.activeInHierarchy 
                && !_view.SettingsMenuView.gameObject.activeInHierarchy)
            {
                _view.PauseMenuView.gameObject.SetActive(false);
                Time.timeScale = 1;
                _model.CurrentStateGame = StateGame.InGame;
            }
        }

        private void TurnOnPause()
        {
            
        }

        private void TurnOffPause()
        {

        }
    }
}
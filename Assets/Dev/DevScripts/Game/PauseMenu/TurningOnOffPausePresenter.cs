using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.Pause
{
    public class TurningOnOffPausePresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public TurningOnOffPausePresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.PressedPause += OnTurnPause;
        }

        public void Unsubscribe()
        {
            _model.PressedPause -= OnTurnPause;
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
    }
}
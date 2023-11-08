using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.Pause
{
    public class PressingEscapePresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public PressingEscapePresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.PressedEscape += OnPressEscape;
        }

        public void Unsubscribe()
        {
            _model.PressedEscape -= OnPressEscape;
        }

        private void OnPressEscape()
        {
            if (_view.LevelsMenuView.gameObject.activeInHierarchy)
            {
                _view.LevelsMenuView.gameObject.SetActive(false);
                return;
            }

            if (_view.SettingsMenuView.gameObject.activeInHierarchy)
            {
                _view.SettingsMenuView.gameObject.SetActive(false);
                return;
            }

            if (_model.CurrentStateGame == StateGame.InGame)
            {
                _model.PauseModel.TurnOnPause();
                return;
            }

            if(_model.CurrentStateGame == StateGame.OnPause)
            {
                _model.PauseModel.TurnOffPause();
                return;
            }
        }
    }
}
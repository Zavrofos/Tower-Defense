using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.SettingsMenu
{
    public class OpenSettingsMenuPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public OpenSettingsMenuPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.SettingsModel.OpenedSettingMenu += OpenSettingsMenu;
        }

        public void Unsubscribe()
        {
            _model.SettingsModel.OpenedSettingMenu -= OpenSettingsMenu;
        }

        private void OpenSettingsMenu()
        {
            if(_model.CurrentStateGame == StateGame.OnPause)
            {
                _view.PauseMenuView.PouseWindow.SetActive(false);
            }
            _view.SettingsMenuView.gameObject.SetActive(true);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class OpenSettingsMenuPresenterInGame : IPresenter
    {
        private LevelViewDev _view;

        public OpenSettingsMenuPresenterInGame(LevelViewDev view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.PouseMenu.OptionsButton.onClick.AddListener(OnOpenSettingsMenu);
        }

        public void Unsubscribe()
        {
            _view.PouseMenu.OptionsButton.onClick.RemoveListener(OnOpenSettingsMenu);
        }

        private void OnOpenSettingsMenu()
        {
            GameManagerDev.Instance.View.SettingsMenuView.gameObject.SetActive(true);
            _view.PouseMenu.PouseMenuWindow.SetActive(false);
        }
    }
}
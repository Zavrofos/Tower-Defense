using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class CloseSettingsMenuPresenterInGame : IPresenter
    {
        private LevelViewDev _view;

        public CloseSettingsMenuPresenterInGame(LevelViewDev view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            GameManagerDev.Instance.View.SettingsMenuView.CloseSettingsWindow.onClick.AddListener(OnCloseWindow);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.SettingsMenuView.CloseSettingsWindow.onClick.RemoveListener(OnCloseWindow);
        }

        private void OnCloseWindow()
        {
            GameManagerDev.Instance.View.SettingsMenuView.gameObject.SetActive(false);
            _view.PouseMenu.PouseMenuWindow.SetActive(true);
        }
    }
}
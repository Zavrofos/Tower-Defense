using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.OptionsMenu
{
    public class CloseSettingsMenuPresenter : IPresenter
    {
        private SettingsMenuView _view;

        public CloseSettingsMenuPresenter(SettingsMenuView view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.CloseSettingsWindow.onClick.AddListener(OnCloseWindow);
        }

        public void Unsubscribe()
        {
            _view.CloseSettingsWindow.onClick.RemoveListener(OnCloseWindow);
        }

        private void OnCloseWindow()
        {
            _view.gameObject.SetActive(false);
        }
    }
}
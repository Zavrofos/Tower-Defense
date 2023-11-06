using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class OpenOptionsMenuPresenter : IPresenter
    {
        private MainMenuView _view;

        public OpenOptionsMenuPresenter(MainMenuView view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.OptionsButton.onClick.AddListener(OnOpenSettionsWindow);
        }

        public void Unsubscribe()
        {
            _view.OptionsButton.onClick.RemoveListener(OnOpenSettionsWindow);
        }

        private void OnOpenSettionsWindow()
        {
            GameManagerDev.Instance.View.SettingsMenuView.gameObject.SetActive(true);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class OpenSettingsMenuPresenterInPause : IPresenter
    {
        public void Subscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.SettingsButton.onClick.AddListener(OnOpenSettingsMenu);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.SettingsButton.onClick.RemoveListener(OnOpenSettingsMenu);
        }

        private void OnOpenSettingsMenu()
        {
            GameManagerDev.Instance.View.SettingsMenuView.gameObject.SetActive(true);
            GameManagerDev.Instance.View.PauseMenuView.PouseWindow.SetActive(false);
        }
    }
}
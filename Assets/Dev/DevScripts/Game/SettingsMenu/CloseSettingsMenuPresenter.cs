using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class CloseSettingsMenuPresenter : IPresenter
    {
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
            if(GameManagerDev.Instance.Model.CurrentStateGame == StateGame.OnPause)
            {
                GameManagerDev.Instance.View.PauseMenuView.PouseWindow.SetActive(true);
            }
            GameManagerDev.Instance.View.SettingsMenuView.gameObject.SetActive(false);
        }
    }
}
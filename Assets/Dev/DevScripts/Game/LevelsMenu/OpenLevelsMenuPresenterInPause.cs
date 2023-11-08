using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class OpenLevelsMenuPresenterInPause : IPresenter
    {
        public void Subscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.SelectALevelButton.onClick.AddListener(OnOpenLevelsMenu);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.SelectALevelButton.onClick.RemoveListener(OnOpenLevelsMenu);
        }

        private void OnOpenLevelsMenu()
        {
            GameManagerDev.Instance.View.LevelsMenuView.gameObject.SetActive(true);
            GameManagerDev.Instance.View.PauseMenuView.PouseWindow.SetActive(false);
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.PauseMenu
{
    public class ClosePauseMenuPresenter : IPresenter
    {
        public void Subscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.CloseWindowButton.onClick.AddListener(OnClosePauseWindow);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.CloseWindowButton.onClick.RemoveListener(OnClosePauseWindow);
        }

        private void OnClosePauseWindow()
        {
            Time.timeScale = 1;
            GameManagerDev.Instance.View.PauseMenuView.gameObject.SetActive(false);
            GameManagerDev.Instance.Model.CurrentStateGame = StateGame.InGame;
        }
    }
}
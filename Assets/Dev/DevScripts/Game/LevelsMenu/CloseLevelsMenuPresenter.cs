using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class CloseLevelsMenuPresenter : IPresenter
    {
        public void Subscribe()
        {
            GameManagerDev.Instance.View.LevelsMenuView.CloseWindowButton.onClick.AddListener(OnCloseWindow);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.LevelsMenuView.CloseWindowButton.onClick.RemoveListener(OnCloseWindow);
        }

        private void OnCloseWindow()
        {
            if(GameManagerDev.Instance.Model.CurrentStateGame == StateGame.OnPause)
            {
                GameManagerDev.Instance.View.PauseMenuView.PouseWindow.SetActive(true);
            }
            GameManagerDev.Instance.View.LevelsMenuView.gameObject.SetActive(false);
        }
    }
}
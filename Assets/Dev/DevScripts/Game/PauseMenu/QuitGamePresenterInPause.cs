using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.PauseMenu
{
    public class QuitGamePresenterInPause : IPresenter
    {
        public void Subscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.QuitButton.onClick.AddListener(OnQuitGame);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.QuitButton.onClick.AddListener(OnQuitGame);
        }

        private void OnQuitGame()
        {
            SaveSystem.SaveLevels(LevelsManager.Instance.Levels);
            Debug.Log("Quit!!!");
            Application.Quit();
        }
    }
}
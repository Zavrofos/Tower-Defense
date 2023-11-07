using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class CloseLevelsMenuPresenterInGame : IPresenter
    {
        private LevelViewDev _view;

        public CloseLevelsMenuPresenterInGame(LevelViewDev view)
        {
            _view = view;
        }

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
            GameManagerDev.Instance.View.LevelsMenuView.gameObject.SetActive(false);
            _view.PouseMenu.PouseMenuWindow.SetActive(true);
        }
    }
}
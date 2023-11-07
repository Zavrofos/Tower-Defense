using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Levels
{
    public class OpenLevelsMenuPresenterInGame : IPresenter
    {
        private LevelViewDev _view;

        public OpenLevelsMenuPresenterInGame(LevelViewDev view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.PouseMenu.SelectALevelButton.onClick.AddListener(OnOpenLevelsMenu);
        }

        public void Unsubscribe()
        {
            _view.PouseMenu.SelectALevelButton.onClick.RemoveListener(OnOpenLevelsMenu);
        }

        private void OnOpenLevelsMenu()
        {
            GameManagerDev.Instance.View.LevelsMenuView.gameObject.SetActive(true);
            _view.PouseMenu.PouseMenuWindow.SetActive(false);
        }
    }
}
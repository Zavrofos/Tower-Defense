using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class OpenLevelsMenuPresenterInMainMenu : IPresenter
    {
        private MainMenuView _view;

        public OpenLevelsMenuPresenterInMainMenu(MainMenuView view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.LevelsGameButton.onClick.AddListener(OnOpenLevelsMenu);
        }

        public void Unsubscribe()
        {
            _view.LevelsGameButton.onClick.RemoveListener(OnOpenLevelsMenu);
        }

        private void OnOpenLevelsMenu()
        {
            GameManagerDev.Instance.View.LevelsMenuView.gameObject.SetActive(true);
        }
    }
}
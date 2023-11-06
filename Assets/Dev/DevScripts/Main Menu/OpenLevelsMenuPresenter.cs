using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class OpenLevelsMenuPresenter : IPresenter
    {
        private MainMenuView _view;

        public OpenLevelsMenuPresenter(MainMenuView view)
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
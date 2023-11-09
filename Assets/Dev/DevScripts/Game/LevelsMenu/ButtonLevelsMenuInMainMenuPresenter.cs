using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class ButtonLevelsMenuInMainMenuPresenter : IPresenter
    {
        private MainMenuView _view;
        private GameModel _model;

        public ButtonLevelsMenuInMainMenuPresenter(MainMenuView view, GameModel model)
        {
            _view = view;
            _model = model;
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
            _model.LevelsManager.OpenLevelsMenu();
        }
    }
}
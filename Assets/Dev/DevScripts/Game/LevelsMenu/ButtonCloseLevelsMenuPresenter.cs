using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.LevelsMenu
{
    public class ButtonCloseLevelsMenuPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public ButtonCloseLevelsMenuPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.LevelsMenuView.CloseWindowButton.onClick.AddListener(CloseWindowLevelsMenu);
        }

        public void Unsubscribe()
        {
            _view.LevelsMenuView.CloseWindowButton.onClick.AddListener(CloseWindowLevelsMenu);
        }

        private void CloseWindowLevelsMenu()
        {
            _model.LevelsManager.CloseLevelsMenu();
        }
    }
}
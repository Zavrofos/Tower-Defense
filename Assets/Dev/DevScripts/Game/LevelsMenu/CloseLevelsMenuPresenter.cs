using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.LevelsMenu
{
    public class CloseLevelsMenuPresenter : IPresenter
    {
        private LevelsMenuView _view;

        public CloseLevelsMenuPresenter(LevelsMenuView view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.CloseWindowButton.onClick.AddListener(OnCloseWindow);
        }

        public void Unsubscribe()
        {
            _view.CloseWindowButton.onClick.RemoveListener(OnCloseWindow);
        }

        private void OnCloseWindow()
        {
            GameManagerDev.Instance.View.LevelsMenuView.gameObject.SetActive(false);
        }
    }
}
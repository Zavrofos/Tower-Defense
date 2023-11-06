using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class PlayGamePresenter : IPresenter
    {
        private MainMenuView _view;
        private GameModel _model;

        public PlayGamePresenter(MainMenuView view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.PlayGameButton.onClick.AddListener(OnPlayGame);
        }

        public void Unsubscribe()
        {
            _view.PlayGameButton.onClick.RemoveListener(OnPlayGame);
        }

        private void OnPlayGame()
        {
            GameManager.Instance.CurrentLevel = 1;
            SceneManager.LoadScene("GameLevel1");
        }
    }
}
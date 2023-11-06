using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class PlayGamePresenter : IPresenter
    {
        private GameModel _model;
        private MainMenuView _view;

        public PlayGamePresenter(GameModel model, MainMenuView view)
        {
            _model = model;
            _view = view;
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
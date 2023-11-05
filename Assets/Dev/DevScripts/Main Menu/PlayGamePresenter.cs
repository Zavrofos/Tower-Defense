using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Main_Menu
{
    public class PlayGamePresenter : IPresenter
    {
        private GameView _gameView;
        private GameModel _gameModel;

        public PlayGamePresenter(GameView gameView, GameModel gameModel)
        {
            _gameView = gameView;
            _gameModel = gameModel;
        }

        public void Subscribe()
        {
            _gameView.MainMenuView.PlayGameButton.onClick.AddListener(OnPlayGame);
        }

        public void Unsubscribe()
        {
            _gameView.MainMenuView.PlayGameButton.onClick.RemoveListener(OnPlayGame);
        }

        private void OnPlayGame()
        {

        }
    }
}
using Assets.Dev.DevScripts.Main_Menu.LevelsMenu;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.LevelsMenu
{
    public class InitializeLevelMenuPresenter : IPresenter
    {
        private GameModel _model;
        private GameViewDev _view;

        public InitializeLevelMenuPresenter(GameModel model, GameViewDev view)
        {
            _model = model;
            _view = view;
        }

        public void Subscribe()
        {
            _model.Initialized += OnInitializeLevelsMenu;
        }

        public void Unsubscribe()
        {
            _model.Initialized += OnInitializeLevelsMenu;
        }

        private void OnInitializeLevelsMenu()
        {
            bool isFirstLevel = true;
            foreach(var levelScene in _view.LevelsScenes)
            {
                LevelDev level = new LevelDev(levelScene.name);
                LevelBoxView levelBox = GameObject.Instantiate(_view.LevelsMenuView.LevelBoxPrefab, _view.LevelsMenuView.Conteiner);
                levelBox.LevelNumberText.text = levelScene.name;

                if (isFirstLevel)
                {
                    level.IsBlock = false;
                    levelBox.Block.gameObject.SetActive(false);
                    isFirstLevel = false;
                }

                _model.LevelsManager.Levels.Add(level);
                _view.LevelsMenuView.Levels.Add(levelBox);
            }
        }
    }
}
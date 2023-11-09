﻿using Assets.Dev.DevScripts;
using Assets.Dev.DevScripts.Game;
using Assets.Dev.DevScripts.Game.LevelsMenu;
using UnityEngine;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class AddingLevelPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public AddingLevelPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }
        
        public void Subscribe()
        {
            _model.LevelsManager.AddedLevel += OnAddedLevel;
        }

        public void Unsubscribe()
        {
            _model.LevelsManager.AddedLevel -= OnAddedLevel;
        }
        
        private void OnAddedLevel(LevelModel level)
        {
            var levelBox =
                Object.Instantiate(_view.LevelsMenuView.LevelBoxPrefab, _view.LevelsMenuView.Conteiner);
            levelBox.LevelNumberText.text = level.NumberLevel;
            _view.LevelsMenuView.Levels.Add(level.NumberLevel, levelBox);
        }
    }
}
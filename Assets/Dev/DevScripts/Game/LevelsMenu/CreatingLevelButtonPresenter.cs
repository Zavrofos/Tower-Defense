﻿using System.Collections.Generic;
using UnityEngine;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class CreatingLevelButtonPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;
        private Dictionary<LevelBoxView, List<IPresenter>> _levelBoxesPresenters = new();

        public CreatingLevelButtonPresenter(GameViewDev view, GameModel model)
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

            List<IPresenter> presenters = new List<IPresenter>()
            {
                new ClickLevelButtonPresenter(levelBox, _model)
            };
            
            _levelBoxesPresenters.Add(levelBox, presenters);

            foreach (var presenter in presenters)
            {
                presenter.Subscribe();
            }

            _model.LevelsManager.Levels.Add(level.NumberLevel, level);
        }
    }
}
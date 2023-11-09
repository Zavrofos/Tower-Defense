using System.Collections.Generic;
using Dev.DevScripts.SaveSystem;
using UnityEngine;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class InitializeLevelMenuPresenter : IPresenter
    {
        private GameModel _model;
        private GameViewDev _view;
        private Dictionary<LevelModel, List<IPresenter>> _levelsPresenters = new();

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
            var data = SaveManager.LoadLevels();
            for (int i = 0; i < _view.LevelsScenes.Count; i++)
            {
                var level = new LevelModel((i + 1).ToString());
                _model.LevelsManager.AddLevel(level);

                List<IPresenter> levelPresenters = new List<IPresenter>()
                {
                    new OpeningLevelPresenter(_view, level),
                    new PlayLevelPresenter(level, _model)
                };

                _levelsPresenters.Add(level, levelPresenters);

                foreach (var presenter in levelPresenters)
                {
                    presenter.Subscribe();
                }
                
                if (data != null && data.OpenLevels[i] || data == null && i == 0)
                {
                    level.OpenLevel();
                }
            }
        }
    }
}
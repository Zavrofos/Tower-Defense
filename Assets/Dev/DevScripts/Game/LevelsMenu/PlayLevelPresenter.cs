using Assets.Dev.DevScripts;
using UnityEngine.SceneManagement;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class PlayLevelPresenter : IPresenter
    {
        private LevelModel _model;
        private GameModel _gameModel;

        public PlayLevelPresenter(LevelModel model, GameModel gameModel)
        {
            _model = model;
            _gameModel = gameModel;
        }

        public void Subscribe()
        {
            _model.PlayedLevel += OnPlayLevel;
        }

        public void Unsubscribe()
        {
            _model.PlayedLevel -= OnPlayLevel;
        }

        private void OnPlayLevel()
        {
            if (_model.IsBlock) return;
            _gameModel.LevelsManager.CloseLevelsMenu();
            SceneManager.LoadScene(int.Parse(_model.NumberLevel));
        }
    }
}
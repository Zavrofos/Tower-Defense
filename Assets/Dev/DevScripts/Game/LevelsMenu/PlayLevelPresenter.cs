using Assets.Scripts;
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
            _gameModel.PauseModel.TurnOffPause();
            _gameModel.CurrentLevel = int.Parse(_model.NumberLevel);
            GameManager.Instance.CurrentLevel = int.Parse(_model.NumberLevel); // Временно
            SceneManager.LoadScene(int.Parse(_model.NumberLevel));
        }
    }
}
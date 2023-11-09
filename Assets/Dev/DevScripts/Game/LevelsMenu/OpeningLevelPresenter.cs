using Assets.Dev.DevScripts;
using Assets.Dev.DevScripts.Game;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class OpeningLevelPresenter : IPresenter
    {
        private GameViewDev _view;
        private LevelModel _model;

        public OpeningLevelPresenter(GameViewDev view, LevelModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.OpenedLevel += OnOpenLevel;
        }

        public void Unsubscribe()
        {
            _model.OpenedLevel -= OnOpenLevel;
        }

        private void OnOpenLevel()
        {
            _model.IsBlock = false;
            _view.LevelsMenuView.Levels[_model.NumberLevel].Block.SetActive(false);
        }
    }
}
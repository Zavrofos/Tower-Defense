

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class CloseLevelsMenuPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public CloseLevelsMenuPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.LevelsManager.ClosedLevelsMenu += OnCloseWindow;
        }

        public void Unsubscribe()
        {
            _model.LevelsManager.ClosedLevelsMenu -= OnCloseWindow;
        }

        private void OnCloseWindow()
        {
            if(_model.CurrentStateGame == StateGame.OnPause)
            {
                _view.PauseMenuView.PouseWindow.SetActive(true);
            }

            _view.LevelsMenuView.gameObject.SetActive(false);
        }
    }
}
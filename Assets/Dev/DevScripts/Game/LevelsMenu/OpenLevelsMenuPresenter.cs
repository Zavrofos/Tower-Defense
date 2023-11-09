

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class OpenLevelsMenuPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public OpenLevelsMenuPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.LevelsManager.OpenedLevelsMenu += OnOpenLevelsMenu;
        }

        public void Unsubscribe()
        {
            _model.LevelsManager.OpenedLevelsMenu -= OnOpenLevelsMenu;
        }

        private void OnOpenLevelsMenu()
        {
            if(_model.CurrentStateGame == StateGame.OnPause)
            {
                _view.PauseMenuView.PouseWindow.SetActive(false);
            }

            _view.LevelsMenuView.gameObject.SetActive(true);
        }
    }
}
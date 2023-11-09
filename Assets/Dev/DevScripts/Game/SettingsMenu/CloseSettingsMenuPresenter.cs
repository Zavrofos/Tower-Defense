

namespace Dev.DevScripts.Game.SettingsMenu
{
    public class CloseSettingsMenuPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public CloseSettingsMenuPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.SettingsModel.ClosedSettingsMenu += CloseSettingsMenu;
        }

        public void Unsubscribe()
        {
            _model.SettingsModel.ClosedSettingsMenu -= CloseSettingsMenu;
        }

        private void CloseSettingsMenu()
        {
            if(_model.CurrentStateGame == StateGame.OnPause)
            {
                _view.PauseMenuView.PouseWindow.SetActive(true);
            }
            _view.SettingsMenuView.gameObject.SetActive(false);
        }
    }
}
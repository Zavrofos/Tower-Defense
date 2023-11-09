namespace Dev.DevScripts.Game
{
    public class PressingEscapePresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public PressingEscapePresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.PressedEscape += OnPressEscape;
        }

        public void Unsubscribe()
        {
            _model.PressedEscape -= OnPressEscape;
        }

        private void OnPressEscape()
        {
            if (_view.LevelsMenuView.gameObject.activeInHierarchy)
            {
                _model.LevelsManager.CloseLevelsMenu();
                return;
            }

            if (_view.SettingsMenuView.gameObject.activeInHierarchy)
            {
                _model.SettingsModel.CloseSettingsMenu();
                return;
            }

            if (_model.CurrentStateGame == StateGame.InGame)
            {
                _model.PauseModel.TurnOnPause();
                return;
            }

            if(_model.CurrentStateGame == StateGame.OnPause)
            {
                _model.PauseModel.TurnOffPause();
                return;
            }
        }
    }
}
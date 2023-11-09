

namespace Dev.DevScripts.Game.SettingsMenu
{
    public class ButtonCloseSettingsMenuPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public ButtonCloseSettingsMenuPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.SettingsMenuView.CloseSettingsWindow.onClick.AddListener(CloseSettingsWindow);
        }

        public void Unsubscribe()
        {
            _view.SettingsMenuView.CloseSettingsWindow.onClick.RemoveListener(CloseSettingsWindow);
        }

        private void CloseSettingsWindow()
        {
            _model.SettingsModel.CloseSettingsMenu();
        }
    }
}
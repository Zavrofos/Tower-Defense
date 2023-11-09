

namespace Dev.DevScripts.Game.SettingsMenu
{
    public class ButtonOpenSettingsMenuInPausePresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public ButtonOpenSettingsMenuInPausePresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.PauseMenuView.SettingsButton.onClick.AddListener(OnOpenSettingsMenu);
        }

        public void Unsubscribe()
        {
            _view.PauseMenuView.SettingsButton.onClick.RemoveListener(OnOpenSettingsMenu);
        }

        private void OnOpenSettingsMenu()
        {
            _model.SettingsModel.OpenSettingsMenu();
        }
    }
}
using Dev.DevScripts.Main_Menu;

namespace Dev.DevScripts.Game.SettingsMenu
{
    public class ButtonOpenSettingsMenuInMainMenuPresenter : IPresenter
    {
        private MainMenuView _view;
        private GameModel _model;

        public ButtonOpenSettingsMenuInMainMenuPresenter(MainMenuView view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.OptionsButton.onClick.AddListener(OnOpenSettionsWindow);
        }

        public void Unsubscribe()
        {
            _view.OptionsButton.onClick.RemoveListener(OnOpenSettionsWindow);
        }

        private void OnOpenSettionsWindow()
        {
            _model.SettingsModel.OpenSettingsMenu();
        }
    }
}
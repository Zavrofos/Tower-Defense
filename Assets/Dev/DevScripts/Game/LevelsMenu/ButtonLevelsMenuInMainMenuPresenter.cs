using Dev.DevScripts.Main_Menu;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class ButtonLevelsMenuInMainMenuPresenter : IPresenter
    {
        private MainMenuView _view;
        private GameModel _model;

        public ButtonLevelsMenuInMainMenuPresenter(MainMenuView view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.LevelsGameButton.onClick.AddListener(OnOpenLevelsMenu);
        }

        public void Unsubscribe()
        {
            _view.LevelsGameButton.onClick.RemoveListener(OnOpenLevelsMenu);
        }

        private void OnOpenLevelsMenu()
        {
            _model.LevelsManager.OpenLevelsMenu();
        }
    }
}
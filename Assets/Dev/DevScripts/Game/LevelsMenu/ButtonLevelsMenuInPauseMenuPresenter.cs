

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class ButtonLevelsMenuInPauseMenuPresenter : IPresenter
    {
        private GameViewDev _view;
        private GameModel _model;

        public ButtonLevelsMenuInPauseMenuPresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.PauseMenuView.SelectALevelButton.onClick.AddListener(OnOpenLevelsMenu);
        }

        public void Unsubscribe()
        {
            _view.PauseMenuView.SelectALevelButton.onClick.RemoveListener(OnOpenLevelsMenu);
        }

        private void OnOpenLevelsMenu()
        {
            _model.LevelsManager.OpenLevelsMenu();
        }
    }
}
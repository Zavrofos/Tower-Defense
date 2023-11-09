namespace Dev.DevScripts.Game.LevelsMenu
{
    public class ClickLevelButtonPresenter : IPresenter
    {
        private LevelBoxView _view;
        private GameModel _model;

        public ClickLevelButtonPresenter(LevelBoxView view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.LevelButton.onClick.AddListener(OnButtonLevelClick);
        }

        public void Unsubscribe()
        {
            _view.LevelButton.onClick.RemoveListener(OnButtonLevelClick);
        }

        private void OnButtonLevelClick()
        {
            _model.LevelsManager.Levels[_view.LevelNumberText.text].PlayLevel();
        }
    }
}

using UnityEngine;

namespace Dev.DevScripts.Game.PauseMenu
{
    public class TurnOnPausePresenter : IPresenter
    {
        public GameViewDev _view;
        public GameModel _model;

        public TurnOnPausePresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.PauseModel.TurnedOnPause += TurnOnPause;
        }

        public void Unsubscribe()
        {
            _model.PauseModel.TurnedOnPause -= TurnOnPause;
        }

        private void TurnOnPause()
        {
            Time.timeScale = 0;
            _model.CurrentStateGame = StateGame.OnPause;
            _view.PauseMenuView.gameObject.SetActive(true);
        }
    }
}
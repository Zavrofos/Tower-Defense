
using UnityEngine;

namespace Dev.DevScripts.Game.PauseMenu
{
    public class TurnOffPausePresenter : IPresenter
    {
        public GameViewDev _view;
        public GameModel _model;

        public TurnOffPausePresenter(GameViewDev view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.PauseModel.TurnedOffPause += TurnOffPause;
            _view.PauseMenuView.CloseWindowButton.onClick.AddListener(TurnOffPause);
        }

        public void Unsubscribe()
        {
            _model.PauseModel.TurnedOffPause -= TurnOffPause;
            _view.PauseMenuView.CloseWindowButton.onClick.RemoveListener(TurnOffPause);
        }

        private void TurnOffPause()
        {
            Time.timeScale = 1;
            _model.CurrentStateGame = StateGame.InGame;
            _view.PauseMenuView.gameObject.SetActive(false);
        }
    }
}
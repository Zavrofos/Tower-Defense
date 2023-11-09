using Assets.Scripts;
using UnityEngine.SceneManagement;

namespace Dev.DevScripts.Main_Menu
{
    public class PlayGamePresenter : IPresenter
    {
        private MainMenuView _view;

        public PlayGamePresenter(MainMenuView view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.PlayGameButton.onClick.AddListener(OnPlayGame);
        }

        public void Unsubscribe()
        {
            _view.PlayGameButton.onClick.RemoveListener(OnPlayGame);
        }

        private void OnPlayGame()
        {
            GameManager.Instance.CurrentLevel = 1;
            SceneManager.LoadScene("GameLevel1");
        }
    }
}
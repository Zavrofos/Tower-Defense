using Assets.Dev.DevScripts;
using UnityEngine;

namespace Dev.DevScripts.Main_Menu
{
    public class QuitGamePresenter : IPresenter
    {
        private MainMenuView _view;

        public QuitGamePresenter(MainMenuView view)
        {
            _view = view;
        }

        public void Subscribe()
        {
            _view.QuitGameButton.onClick.AddListener(OnQuitGame);
        }

        public void Unsubscribe()
        {
            _view.QuitGameButton.onClick.RemoveListener(OnQuitGame);
        }

        private void OnQuitGame()
        {
            Debug.Log("Quit!!!");
            Application.Quit();
        }
    }
}
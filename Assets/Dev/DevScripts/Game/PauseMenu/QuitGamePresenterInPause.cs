using Assets.Dev.DevScripts;
using UnityEngine;

namespace Dev.DevScripts.Game.PauseMenu
{
    public class QuitGamePresenterInPause : IPresenter
    {
        public void Subscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.QuitButton.onClick.AddListener(OnQuitGame);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.QuitButton.onClick.AddListener(OnQuitGame);
        }

        private void OnQuitGame()
        {
            Debug.Log("Quit!!!");
            Application.Quit();
        }
    }
}
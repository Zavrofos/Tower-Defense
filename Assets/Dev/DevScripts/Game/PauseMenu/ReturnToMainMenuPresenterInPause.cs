using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dev.DevScripts.Game.PauseMenu
{
    public class ReturnToMainMenuPresenterInPause : IPresenter
    {
        public void Subscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.MainMenuButton.onClick.AddListener(OnReturnToMainMenu);
        }

        public void Unsubscribe()
        {
            GameManagerDev.Instance.View.PauseMenuView.MainMenuButton.onClick.AddListener(OnReturnToMainMenu);
        }

        private void OnReturnToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1;
            GameManagerDev.Instance.Model.CurrentStateGame = StateGame.InGame;
            GameManagerDev.Instance.View.PauseMenuView.gameObject.SetActive(false);
        }
    }
}
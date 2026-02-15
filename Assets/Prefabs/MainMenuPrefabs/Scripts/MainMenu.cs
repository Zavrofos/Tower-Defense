using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private SettingsMenu _settingsMenu;

    private void OnPlay()
    {
        SceneManager.LoadScene("GameHub");
    }

    private void OnOpenOptions()
    {
        _settingsMenu.gameObject.SetActive(true);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlay);
        _settingsButton.onClick.AddListener(OnOpenOptions);
        _quitButton.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlay);
        _settingsButton.onClick.RemoveListener(OnOpenOptions);
        _quitButton.onClick.RemoveListener(OnQuit);
    }

}

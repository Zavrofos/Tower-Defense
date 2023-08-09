using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    public Button _levelButton;
    public GameObject _block;
    public TMP_Text LabelText;
    public Level Level;

    private void LoadScene()
    {
        if (Level.IsOpen)
        {
            FindObjectOfType<GameManager>().CurrentLevel = int.Parse(Level.Label);
            SceneManager.LoadScene("GameLevel" + Level.Label);
            Time.timeScale = 1;
        }
    }

    public void OpenLevel()
    {
        _block.SetActive(false);
    }

    private void OnEnable()
    {
        _levelButton.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(LoadScene);
    }
}

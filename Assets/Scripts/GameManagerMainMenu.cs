using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMainMenu : MonoBehaviour
{
    [SerializeField] private Transform _conteiner;
    [SerializeField] private LevelView _levelViewPref;

    private void Start()
    {
        if (LevelsManager.Instance != null)
        {
            for (int i = 0; i < LevelsManager.Instance.Levels.Count; i++)
            {
                Level level = LevelsManager.Instance.Levels[i];
                LevelView levelPref = Instantiate(_levelViewPref, _conteiner);
                levelPref.Level = level;
                levelPref.LabelText.text = level.Label;
                if (level.IsOpen) levelPref.OpenLevel();
            }
        }
        else
        {
            Debug.Log("Õóéíÿ");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerMainMenu : MonoBehaviour
{
    [SerializeField] private Transform _conteiner;
    [SerializeField] private LevelView _levelViewPref;
    private LevelsManager levelsManager;

    private void Start()
    {
        levelsManager = FindObjectOfType<LevelsManager>();
        if(levelsManager != null)
        {
            for (int i = 0; i < levelsManager.Levels.Count; i++)
            {
                Level level = levelsManager.Levels[i];
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

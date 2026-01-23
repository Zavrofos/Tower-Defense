using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public static LevelsManager Instance;
    public List<Level> Levels;
    public int CountLevels;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeLevels();
            return;
        }
        Destroy(gameObject);
    }

    private void InitializeLevels()
    {
        LevelsData data = SaveSystem.LoadLevels(CountLevels);
        Levels = new List<Level>();
        if (data != null)
        {
            for(int i = 0; i < CountLevels; i++)
            {
                Levels.Add(new Level(i + 1) { IsOpen = data.OpenLevels[i] } );
            }
            return;
        }

        Levels = new List<Level>();
        for (int i = 0; i < CountLevels; i++)
        {
            Level level = new Level(i + 1);
            Levels.Add(level);
        }
        Levels[0].OpenLevel();
    }
}

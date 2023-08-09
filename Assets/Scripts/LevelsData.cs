using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class LevelsData 
{
    public bool[] OpenLevels;

    public LevelsData(List<Level> levels)
    {
        OpenLevels = new bool[levels.Count];
        for(int i = 0; i < levels.Count; i++)
        {
            OpenLevels[i] = levels[i].IsOpen;
        }
    }
}

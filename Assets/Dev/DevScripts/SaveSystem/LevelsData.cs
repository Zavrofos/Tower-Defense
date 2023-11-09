using System;
using System.Collections.Generic;

namespace Dev.DevScripts.SaveSystem
{
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
}

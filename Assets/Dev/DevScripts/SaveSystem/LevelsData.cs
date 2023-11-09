using System;
using System.Collections.Generic;
using Dev.DevScripts.Game.LevelsMenu;

namespace Dev.DevScripts.SaveSystem
{
    [Serializable]
    public class LevelsData 
    {
        public bool[] OpenLevels;
    
        public LevelsData(List<LevelModel> levels)
        {
            OpenLevels = new bool[levels.Count];
            for(int i = 0; i < levels.Count; i++)
            {
                OpenLevels[i] = levels[i].IsBlock;
            }
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dev.DevScripts.Game.LevelsMenu;
using UnityEngine;

namespace Dev.DevScripts.SaveSystem
{
    public static class SaveManager 
    {
        public static void SaveLevels(List<LevelModel> levels)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/game.sav";
            FileStream stream = new FileStream(path, FileMode.Create);
            LevelsData data = new LevelsData(levels);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static LevelsData LoadLevels()
        {
            string path = Application.persistentDataPath + "/game.sav";

            if(File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                LevelsData data = formatter.Deserialize(stream) as LevelsData;
                stream.Close();
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
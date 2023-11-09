using System.Collections.Generic;
using Dev.DevScripts.Game.LevelsMenu;
using Dev.DevScripts.Game.PauseMenu;
using Dev.DevScripts.Game.SettingsMenu;
using UnityEditor;
using UnityEngine;

namespace Dev.DevScripts.Game
{
    public class GameViewDev : MonoBehaviour
    {
        public List<SceneAsset> LevelsScenes;
        public LevelsMenuView LevelsMenuView;
        public SettingsMenuView SettingsMenuView;
        public PauseMenuView PauseMenuView;
    }
}
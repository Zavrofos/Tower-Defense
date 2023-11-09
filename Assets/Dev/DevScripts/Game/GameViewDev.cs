using System.Collections;
using System.Collections.Generic;
using Dev.DevScripts.Game.LevelsMenu;
using UnityEditor;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game
{
    public class GameViewDev : MonoBehaviour
    {
        public List<SceneAsset> LevelsScenes;
        public LevelsMenuView LevelsMenuView;
        public SettingsMenuView SettingsMenuView;
        public PauseMenuView PauseMenuView;
    }
}
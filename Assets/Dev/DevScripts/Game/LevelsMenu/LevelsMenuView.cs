using System.Collections.Generic;
using Assets.Dev.DevScripts.Game.LevelsMenu;
using UnityEngine;
using UnityEngine.UI;

namespace Dev.DevScripts.Game.LevelsMenu
{
    public class LevelsMenuView : MonoBehaviour
    {
        [HideInInspector] public Dictionary<string, LevelBoxView> Levels = new();

        public Button CloseWindowButton;
        public LevelBoxView LevelBoxPrefab;
        public Transform Conteiner;
    }
}

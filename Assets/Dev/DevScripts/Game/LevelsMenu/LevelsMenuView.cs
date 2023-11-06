using Assets.Dev.DevScripts.Game.LevelsMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenuView : MonoBehaviour
{
    [HideInInspector] public List<LevelBoxView> Levels;

    public Button CloseWindowButton;
    public LevelBoxView LevelBoxPrefab;
    public Transform Conteiner;
}

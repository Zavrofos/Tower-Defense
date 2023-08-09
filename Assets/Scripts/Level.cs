using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level
{
    public string Label;
    public bool IsOpen = false;

    public Level(int label)
    {
        Label = label.ToString();
    }

    public void OpenLevel()
    {
        IsOpen = true;
    }
}

using Assets.Dev.DevScripts.Main_Menu;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    public TMP_Text NameGameText;
    public Button PlayGameButton;
    public Button LevelsGameButton;
    public Button OptionsButton;
    public Button QuitGameButton;
    public List<RotationObjectView> RotationObjects;
}

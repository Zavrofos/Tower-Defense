using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuView : MonoBehaviour
{
    public TMP_Dropdown DropDownGraphics;
    public TMP_Dropdown DropdownResolutions;
    public Toggle ToggleFullscreen;
    public Slider VolumeMusicSlider;
    public Slider VolumeGameSlider;
    public Button CloseSettingsWindow;
    public AudioMixer AudioMixer;
}

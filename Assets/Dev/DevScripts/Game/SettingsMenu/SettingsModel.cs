using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.OptionsMenu
{
    public class SettingsModel 
    {
        public int CurrentIndexQuality;

        public int CurrentResolutionIndex;
        public List<string> Options;
        public List<(int, int)> Resolutions;

        public bool IsFullscreen;

        public float CurrentMusicVolumeValue;

        public float CurrentGameVolumeValue;

        public event Action OpenedSettingMenu;
        public event Action ClosedSettingsMenu;

        public void OpenSettingsMenu()
        {
            OpenedSettingMenu?.Invoke();
        }

        public void CloseSettingsMenu()
        {
            ClosedSettingsMenu?.Invoke();
        }
    }
}
using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.OptionsMenu
{
    public class SetResolutionPresenter : IPresenter
    {
        private SettingsMenuView _view;
        private SettingsModel _model;

        public SetResolutionPresenter(SettingsMenuView view, SettingsModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.DropdownResolutions.onValueChanged.AddListener(OnValueChanged);
        }

        public void Unsubscribe()
        {
            _view.DropdownResolutions.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(int resolutionIndex)
        {
            (int, int) resolution = _model.Resolutions[resolutionIndex];
            Screen.SetResolution(resolution.Item1, resolution.Item2, Screen.fullScreen);
            _model.CurrentResolutionIndex = resolutionIndex;
        }
    }
}
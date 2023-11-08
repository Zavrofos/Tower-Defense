using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Dev.DevScripts.Game.OptionsMenu
{
    public class SetQualityPresenter : IPresenter
    {
        private SettingsMenuView _view;
        private SettingsModel _model;

        public SetQualityPresenter(SettingsMenuView view, SettingsModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.DropDownGraphics.onValueChanged.AddListener(OnValueChanged);
        }

        public void Unsubscribe()
        {
            _view.DropDownGraphics.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(int value)
        {
            QualitySettings.SetQualityLevel(value);
            _model.CurrentIndexQuality = value;
        }
    }
}
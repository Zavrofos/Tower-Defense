using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Dev.DevScripts.Game.OptionsMenu
{
    public class SetVolumeGamePresenter : IPresenter
    {
        private SettingsMenuView _view;
        private SettingsModel _model;

        public SetVolumeGamePresenter(SettingsMenuView view, SettingsModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.VolumeGameSlider.onValueChanged.AddListener(OnValueChanged);
        }

        public void Unsubscribe()
        {
            _view.VolumeGameSlider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _view.AudioMixer.SetFloat("GameVolume", value);
            _model.CurrentGameVolumeValue = value;
        }
    }
}
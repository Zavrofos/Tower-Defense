using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEditor.Progress;

namespace Assets.Dev.DevScripts.Game.OptionsMenu
{
    public class InitializeSettingsMenuPresenter : IPresenter
    {
        private SettingsMenuView _view;
        private GameModel _model;

        public InitializeSettingsMenuPresenter(SettingsMenuView view, GameModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _model.Initialized += OnInitializeQualitySettings;
            _model.Initialized += OnInitializeResolutionsSettings;
            _model.Initialized += OnInitializeFullscreenToggleSettings;
            _model.Initialized += OnInitializeVolumeMusicSliderSettings;
            _model.Initialized += OnInitializeVolumeGameSliderSettings;
        }

        public void Unsubscribe()
        {
            _model.Initialized -= OnInitializeQualitySettings;
            _model.Initialized -= OnInitializeResolutionsSettings;
            _model.Initialized -= OnInitializeFullscreenToggleSettings;
            _model.Initialized -= OnInitializeVolumeMusicSliderSettings;
            _model.Initialized -= OnInitializeVolumeGameSliderSettings;
        }

        private void OnInitializeQualitySettings()
        {
            _view.DropDownGraphics.value = _model.SettingsModel.CurrentIndexQuality;
            QualitySettings.SetQualityLevel(_model.SettingsModel.CurrentIndexQuality);
        }

        private void OnInitializeResolutionsSettings()
        {
            _model.SettingsModel.Options = new();
            _model.SettingsModel.Resolutions = new()
            {
                (1280, 720),
                (1920, 1080),
                (2560, 1440),
                (3840, 2160)
            };

            for (int i = 0; i < _model.SettingsModel.Resolutions.Count; i++)
            {
                string option = _model.SettingsModel.Resolutions[i].Item1 + " x " + _model.SettingsModel.Resolutions[i].Item2;
                _model.SettingsModel.Options.Add(option);

                if (_model.SettingsModel.Resolutions[i].Item1 == Screen.currentResolution.width &&
                    _model.SettingsModel.Resolutions[i].Item2 == Screen.currentResolution.height)
                {
                    _model.SettingsModel.CurrentResolutionIndex = i;
                }
            }

            _view.DropdownResolutions.ClearOptions();
            _view.DropdownResolutions.AddOptions(GameManager.Instance.Options);
            _view.DropdownResolutions.value = GameManager.Instance.ResolutionIndex;
            _view.DropdownResolutions.RefreshShownValue();

            (int, int) resolution = _model.SettingsModel.Resolutions[_model.SettingsModel.CurrentResolutionIndex];
            Screen.SetResolution(resolution.Item1, resolution.Item2, Screen.fullScreen);
        }

        private void OnInitializeFullscreenToggleSettings()
        {
            _model.SettingsModel.IsFullscreen = true;
            _view.ToggleFullscreen.isOn = _model.SettingsModel.IsFullscreen;
            Screen.fullScreen = _model.SettingsModel.IsFullscreen;
        }

        private void OnInitializeVolumeMusicSliderSettings()
        {
            _model.SettingsModel.CurrentMusicVolumeValue = -10;
            _view.VolumeMusicSlider.value = _model.SettingsModel.CurrentMusicVolumeValue;
            _view.AudioMixer.SetFloat("MusicVolume", _model.SettingsModel.CurrentMusicVolumeValue);
        }

        private void OnInitializeVolumeGameSliderSettings()
        {
            _model.SettingsModel.CurrentGameVolumeValue = -10;
            _view.VolumeMusicSlider.value = _model.SettingsModel.CurrentGameVolumeValue;
            _view.AudioMixer.SetFloat("GameVolume", _model.SettingsModel.CurrentGameVolumeValue);
        }
    }
}
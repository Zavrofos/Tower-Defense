using UnityEngine;

namespace Dev.DevScripts.Game.SettingsMenu
{
    public class SetFullscreenPresenter : IPresenter
    {
        private SettingsMenuView _view;
        private SettingsModel _model;

        public SetFullscreenPresenter(SettingsMenuView view, SettingsModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.ToggleFullscreen.onValueChanged.AddListener(OnValueChanged);
        }

        public void Unsubscribe()
        {
            _view.ToggleFullscreen.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
            _model.IsFullscreen = isFullscreen;
        }
    }
}
namespace Dev.DevScripts.Game.SettingsMenu
{
    public class SetVolumeMusicPresenter : IPresenter
    {
        private SettingsMenuView _view;
        private SettingsModel _model;

        public SetVolumeMusicPresenter(SettingsMenuView view, SettingsModel model)
        {
            _view = view;
            _model = model;
        }

        public void Subscribe()
        {
            _view.VolumeMusicSlider.onValueChanged.AddListener(OnValueChanged);
        }

        public void Unsubscribe()
        {
            _view.VolumeMusicSlider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _view.AudioMixer.SetFloat("MusicVolume", value);
            _model.CurrentMusicVolumeValue = value;
        }
    }
}
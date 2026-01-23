using System;
using Assets.Scripts.RepPoolObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using UniRx;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool IsStartGame = true;

        public List<(int, int)> Resolutions { get; private set; } = new()
        {
            (1280, 720),
            (1920, 1080),
            (2560, 1440),
            (3840, 2160)
        };

        public List<string> Options { get; private set; } = new()
        {
            ("1280 x 720"),
            ("1920 x 1080"),
            ("2560 x 1440"),
            ("3840 x 2160")
        };
            
        public float MusicVolumeValue = 0;
        public float GameVolumeValue = 0;
        public int IndexQuality = 0;
        public int ResolutionIndex = 0;
        public bool IsFullscreen = true;

        public int CurrentLevel;

        [SerializeField] private ObjectPooler _objectPooler;
        [SerializeField] private AudioManager _audioManager;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                SetSettingsValues();
                return;
            }
            
            Destroy(gameObject);
        }

        private void Start()
        {
            _objectPooler.Initialize();
            _audioManager.Initialize();
        }

        private void SetSettingsValues()
        {
            SetQualitySettings();
            SetResolutionSettings();
            SetFullScreenSettings();
            
            Observable.NextFrame()
                .Subscribe(_ =>
                {
                    SetVolumeGameSettings();
                    SetVolumeMusicSettings();
                })
                .AddTo(this);
        }

        private void SetQualitySettings()
        {
            IndexQuality = SaveSystem.SaveSystem.GetQuality();
            QualitySettings.SetQualityLevel(IndexQuality);
            SaveSystem.SaveSystem.SaveQuality(IndexQuality);
        }

        private void SetResolutionSettings()
        {
            ResolutionIndex = SaveSystem.SaveSystem.GetResolutions();
            ResolutionIndex = ResolutionIndex == -1 ? GetCurrentScreenResolutions() : ResolutionIndex;
            (int, int) resolution = Resolutions[ResolutionIndex];
            Screen.SetResolution(resolution.Item1, resolution.Item2, Screen.fullScreen);
            SaveSystem.SaveSystem.SaveResolutions(ResolutionIndex);
        }

        private void SetFullScreenSettings()
        {
            IsFullscreen = SaveSystem.SaveSystem.GetFullScreen();
            Screen.fullScreen = IsFullscreen;
            SaveSystem.SaveSystem.SaveFullScreen(IsFullscreen);
        }

        private void SetVolumeMusicSettings()
        {
            MusicVolumeValue = SaveSystem.SaveSystem.GetVolumeMusic();
            _audioManager.AudioMixer.SetFloat("MusicVolume", MusicVolumeValue);
            SaveSystem.SaveSystem.SaveVolumeMusicScreen(MusicVolumeValue);
        }

        private void Test()
        {
            
        }

        private void SetVolumeGameSettings()
        {
            GameVolumeValue = SaveSystem.SaveSystem.GetVolumeGame();
            _audioManager.AudioMixer.SetFloat("GameVolume", GameVolumeValue);
            SaveSystem.SaveSystem.SaveVolumeGameScreen(GameVolumeValue);
        }
        
        private int GetCurrentScreenResolutions()
        {
            for (int i = 0; i < Resolutions.Count; i++)
            {
                if (Resolutions[i].Item1 == Screen.currentResolution.width &&
                    Resolutions[i].Item2 == Screen.currentResolution.height)
                {
                    return i;
                }
            }

            return 1;
        }
    }
}
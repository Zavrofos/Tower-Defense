using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public bool IsStartGame = true;

        public List<(int, int)> Resolutions { get; private set; } 
        public List<string> Options { get; private set; }
        public float MusicVolumeValue = 0;
        public float GameVolumeValue = 0;
        public int IndexQuality = 0;
        public int ResolutionIndex = 0;
        public bool IsFullscreen = true;

        public int CurrentLevel;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                SetStartGameSettings();
                return;
            }
            Destroy(gameObject);
        }


        private void SetStartGameSettings()
        {
            Options = new List<string>();
            Resolutions = new List<(int, int)>()
            {
                (1280, 720),
                (1920, 1080),
                (2560, 1440),
                (3840, 2160)
            };

            for (int i = 0; i < Resolutions.Count; i++)
            {
                string option = Resolutions[i].Item1 + " x " + Resolutions[i].Item2;
                Options.Add(option);

                if (Resolutions[i].Item1 == Screen.currentResolution.width &&
                    Resolutions[i].Item2 == Screen.currentResolution.height)
                {
                    ResolutionIndex = i;
                }
            }
        }
    }
}
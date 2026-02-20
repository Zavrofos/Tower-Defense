using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UIScripts
{
    public class ClickSoundPlayGlobal : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public static ClickSoundPlayGlobal Instance;
        private void Awake()
        {
            if (Instance)
            {
                Destroy(this);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
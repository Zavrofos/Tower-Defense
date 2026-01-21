using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Assets.Scripts.Enums;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //[SerializeField] public AudioSource AudioSourceMelody;
    private List<AudioSource> _currentAudioPlaying;

    private void Awake()

    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            //AudioMelodyPlay();
            _currentAudioPlaying = new List<AudioSource>();
            return;
        }
        Destroy(gameObject);
    }

    //private void Start()
    //{
        
    //}

    public void Initialize()
    {
        AudioMelodyPlay();
    }

    public void AudioMelodyPlay()
    {
        //AudioSourceMelody.Play();
        SoundBox soundBox = (SoundBox)ObjectPooler.Instance.SpawnFromPool("SoundBox", 
            transform.position, 
            transform.rotation);
        soundBox.PlaySound(SoundType.GameMusic);
    }

    public void PauseAudio()
    {
        _currentAudioPlaying.Clear();
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach(var audio in audios)
        {
            if(audio.isPlaying /*&& audio != AudioSourceMelody*/)
            {
                SoundBox soundBox = audio.gameObject.GetComponent<SoundBox>();
                if(soundBox.CurrentAudioClip.SoundCategory == SoundCategory.BackgrounMelody)
                {
                    continue;
                }
                
                audio.Pause();
                _currentAudioPlaying.Add(audio);
            }
        }
    }

    public void PlayAudio()
    {
        foreach(var audio in _currentAudioPlaying)
        {
            audio.Play();
        }
        _currentAudioPlaying.Clear();
    }
}




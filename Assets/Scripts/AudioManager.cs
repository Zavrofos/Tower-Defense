using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] public AudioSource AudioSourceMelody;
    private List<AudioSource> _currentAudioPlaying;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            AudioMelodyPlay();
            _currentAudioPlaying = new List<AudioSource>();
            return;
        }
        Destroy(gameObject);
    }

    public void AudioMelodyPlay()
    {
        AudioSourceMelody.Play();
    }

    public void PauseAudio()
    {
        _currentAudioPlaying.Clear();
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        foreach(var audio in audios)
        {
            if(audio.isPlaying && audio != AudioSourceMelody)
            {
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




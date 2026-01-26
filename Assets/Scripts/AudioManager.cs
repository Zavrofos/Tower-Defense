using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Assets.Scripts.Enums;
using UniRx;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public AudioMixer AudioMixer { get; private set; }
    
    public static AudioManager Instance;
    private List<AudioSource> _currentAudioPlaying;

    private void Awake()

    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            _currentAudioPlaying = new List<AudioSource>();
            return;
        }
        Destroy(gameObject);
    }

    public void Initialize()
    {
        Observable.NextFrame()
            .Subscribe(_ => AudioMelodyPlay())
            .AddTo(this);
    }

    public void AudioMelodyPlay()
    {
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
            if(audio.isPlaying)
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
    
    public float FormatToDb(float value01)
    {
        value01 = Mathf.Clamp(value01, 0.0001f, 1f);
        return Mathf.Log10(value01) * 20f;
    }
}




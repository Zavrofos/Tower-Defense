using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundAudioClip
{
    public SoundType Sound;
    public AudioClip AudioClip;
    public AudioMixerGroup Output;
}

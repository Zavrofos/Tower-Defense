using Assets.Scripts.Enums;
using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundAudioClip
{
    public SoundType Sound;
    public SoundCategory SoundCategory;
    public AudioClip AudioClip;
    public AudioMixerGroup Output;
}

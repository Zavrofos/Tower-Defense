using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class GameAssets : MonoBehaviour
{
    #region Singleton
    public static GameAssets Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            return;
        }

        Destroy(this.gameObject);
    }
    #endregion
    public SoundAudioClip[] SoundAudioClips;
}

[Serializable]
public class SoundAudioClip
{
    public SoundType Sound;
    public AudioClip AudioClip;
    public AudioMixerGroup Output;
}

public enum SoundType
{
    ShootLowBullet,
    ShootMediumBullet,
    Explosion,
    Cold,
    Laser,
    GameMusic
}

public enum BulletType
{
    Low,
    LowPlus,
    Medium,
    MediumPlus,
    Hight,
    HightPlus
}

public enum AbilityType
{
    Rocket,
    Mine
}

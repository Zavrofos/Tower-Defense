using System;
using Assets.Scripts.GlobalShop;
using UnityEngine;

[Serializable]
public class TowerInfo
{
    public GlobalShopItemType Type;
    public GameObject TowerPrefab;
}

public class GameAssets : MonoBehaviour
{
    public TowerInfo[] TowersInfos;

    public AudioClip AcceptAudioUI;
    public AudioClip BackAudioUI;
    public AudioClip CLickAudioUI;
    public AudioClip LifeDamageAudio;
    public AudioClip MoneyAudio;
    public AudioClip PopupShopAudio;
}

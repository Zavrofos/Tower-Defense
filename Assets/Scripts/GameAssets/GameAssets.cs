using System;
using System.Collections.Generic;
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
    public TowerInfo[] TowersInfos;
}

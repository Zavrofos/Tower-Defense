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
}

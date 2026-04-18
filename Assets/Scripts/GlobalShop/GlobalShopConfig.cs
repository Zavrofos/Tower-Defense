using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GlobalShop
{
    [Serializable]
    public class GlobalShopItemInfo
    {
        public Sprite IconShopItem;
        public Sprite IconDescriptionItem;
        public int Price;
        public string NameItemKey;
        public string DescriptionItemKey;
        public GlobalShopItemType Type;
        public bool IsUpgradeType;
        public Sprite UpgradeIcon;
        public Sprite UpgradeDescriptionIcon;
        public int UpgradePrice;
        public string UpgradedNameKey;
        public string UpgradedDescriptionKey;
    }
    
    
    [CreateAssetMenu(menuName = "Scriptables/GlobalShopConfig", fileName = "GlobalShopConfig")]
    public class GlobalShopConfig : ScriptableObject
    {
        public List<GlobalShopItemInfo> GlobalShopItemsInfos;

        public Dictionary<GlobalShopItemType, GlobalShopItemInfo> InfosDic = new Dictionary<GlobalShopItemType, GlobalShopItemInfo>();

        public void Init()
        {
            foreach (var info in GlobalShopItemsInfos)
                InfosDic.TryAdd(info.Type, info);
        }
    }
}
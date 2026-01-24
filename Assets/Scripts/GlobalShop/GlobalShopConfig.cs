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
        public string DescriptionItem;
        public string NameItem;
        public GlobalShopItemType Type;
        
        public bool IsUpgradeType;

        public Sprite UpgradeIcon;
        public int UpgradePrice;
    }
    
    
    [CreateAssetMenu(menuName = "Scriptables/GlobalShopConfig", fileName = "GlobalShopConfig")]
    public class GlobalShopConfig : ScriptableObject
    {
        public List<GlobalShopItemInfo> GlobalShopItemsInfos;
    }
}
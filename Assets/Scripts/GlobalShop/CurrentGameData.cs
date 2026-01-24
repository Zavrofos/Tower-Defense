using System;
using System.Collections.Generic;

namespace Assets.Scripts.GlobalShop
{
    [Serializable]
    public class CurrentGameData
    {
        public int CurrentGlobalMoney = 1000;
        public int CurrentLevel = 1;
        public bool IsRocketAbilityBought;
        public int CountMineBought;
        public int CountResetLevelCoins;
        public TowerData LowTowerData;
        public TowerData MediumTowerData;
        public TowerData HeightTowerData;
        public TowerData ColdTowerData;
        public TowerData LaserTowerData;

        public Dictionary<GlobalShopItemType, TowerData> TowersData;

        public void Init()
        {
            LowTowerData ??= new TowerData() { TowerType = GlobalShopItemType.TowerLow };
            MediumTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerMedium };
            HeightTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerHigh };
            ColdTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerCold };
            LaserTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerLaser };
            
            TowersData = new Dictionary<GlobalShopItemType, TowerData>()
            {
                {GlobalShopItemType.TowerLow, LowTowerData},
                {GlobalShopItemType.TowerMedium, MediumTowerData},
                {GlobalShopItemType.TowerHigh, HeightTowerData},
                {GlobalShopItemType.TowerCold, ColdTowerData},
                {GlobalShopItemType.TowerLaser, LaserTowerData},
            };
        }
    }

    
    [Serializable]
    public class TowerData
    {
        public GlobalShopItemType TowerType;
        public bool IsBought;
        public bool IsUpgradedBought;
    }
}
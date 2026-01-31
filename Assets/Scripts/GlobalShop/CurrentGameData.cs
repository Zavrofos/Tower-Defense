using System;
using System.Collections.Generic;

namespace Assets.Scripts.GlobalShop
{
    [Serializable]
    public class CurrentGameData
    {
        public bool ResetLevelBought;
        public bool IsWinGame;
        public bool IsWinLevel;
        public bool IsGameOverLevel;
        public int CurrentGlobalMoney = 0;
        public int CurrentLevel = 1;
        public bool IsRocketAbilityBought;
        public bool IsMineAbilityBought = true;
        public int CountMineBought = 10;
        public int CountResetLevelCoins;
        public TowerData LowTowerData;
        public TowerData MediumTowerData;
        public TowerData HeightTowerData;
        public TowerData ColdTowerData;
        public TowerData LaserTowerData;

        public Dictionary<GlobalShopItemType, TowerData> TowersData;

        public void Init()
        {
            LowTowerData ??= new TowerData() { TowerType = GlobalShopItemType.TowerLow, IsBought = true};
            MediumTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerMedium, IsBought = true};
            HeightTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerHigh, IsBought = true };
            ColdTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerCold, IsBought = true };
            LaserTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerLaser, IsBought = true };
            
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
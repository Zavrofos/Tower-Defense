using System;

namespace Assets.Scripts.GlobalShop
{
    [Serializable]
    public class CurrentGameData
    {
        public int CurrentGlobalMoney;
        public int CurrentLevel = 1;
        public bool IsRocketAbilityBought;
        public bool CountMineBought;
        public int CountResetLevelCoins;
        public TowerData LowTowerData;
        public TowerData MediumTowerData;
        public TowerData HeightTowerData;
        public TowerData ColdTowerData;
        public TowerData LaserTowerData;
    }

    
    [Serializable]
    public class TowerData
    {
        public GlobalShopItemType TowerType;
        public bool IsBought;
        public bool IsUpgradedBought;
    }
}
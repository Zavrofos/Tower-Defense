using System;
using System.Collections.Generic;

namespace Assets.Scripts.GlobalShop
{
    [Serializable]
    public class CurrentGameData
    {
        public int[] Worlds;
        public LevelRow[] Levels;
        
        public int CurrentGlobalMoney = 0;
        
        public TowerData LowTowerData;
        public TowerData MediumTowerData;
        public TowerData HeightTowerData;
        public TowerData ColdTowerData;
        public TowerData LaserTowerData;
        public TowerData LaserTowerNewData;

        public AbilityData RocketAbility;
        public AbilityData MineAbility;
        public AbilityData MeteorsShowerAbility;
        public AbilityData FoodAbility;
        public AbilityData PocketMoneyAbility;

        public Dictionary<GlobalShopItemType, TowerData> TowersData;
        public Dictionary<GlobalShopItemType, AbilityData> AbilityData;

        public void Init()
        {
            Worlds ??= new int[3];
            Worlds[0] = 1;

            Levels ??= new LevelRow[3];

            for (int i = 0; i < Levels.Length; i++)
            {
                if (Levels[i] == null)
                    Levels[i] = new LevelRow();

                Levels[i].Cols ??= new int[4];
            }

            Levels[0].Cols[0] = 1;
            
            LowTowerData ??= new TowerData() { TowerType = GlobalShopItemType.TowerLow, IsBought = true, IsOpenToBuy = true};
            MediumTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerMedium, IsBought = true, IsOpenToBuy = true};
            HeightTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerHigh};
            ColdTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerCold};
            LaserTowerData ??= new TowerData () { TowerType = GlobalShopItemType.TowerLaser};
            LaserTowerNewData ??= new TowerData () { TowerType = GlobalShopItemType.TowerLaserNew};
            
            TowersData = new Dictionary<GlobalShopItemType, TowerData>()
            {
                {GlobalShopItemType.TowerLow, LowTowerData},
                {GlobalShopItemType.TowerMedium, MediumTowerData},
                {GlobalShopItemType.TowerHigh, HeightTowerData},
                {GlobalShopItemType.TowerCold, ColdTowerData},
                {GlobalShopItemType.TowerLaser, LaserTowerData},
                {GlobalShopItemType.TowerLaserNew, LaserTowerNewData}
            };
            
            RocketAbility ??= new AbilityData()  { AbilityType = GlobalShopItemType.AbilityRocket};
            MineAbility ??= new AbilityData()  { AbilityType = GlobalShopItemType.AbilityMine};
            MeteorsShowerAbility ??= new AbilityData()  { AbilityType = GlobalShopItemType.MeteorShowerAbility};
            FoodAbility ??= new AbilityData()  { AbilityType = GlobalShopItemType.FoodAbility};
            PocketMoneyAbility ??= new AbilityData()  { AbilityType = GlobalShopItemType.MoneyPocketAbility};
            
            AbilityData = new Dictionary<GlobalShopItemType, AbilityData>()
            {
                {GlobalShopItemType.AbilityRocket, RocketAbility},
                {GlobalShopItemType.AbilityMine, MineAbility},
                {GlobalShopItemType.MeteorShowerAbility, MeteorsShowerAbility},
                {GlobalShopItemType.FoodAbility, FoodAbility},
                {GlobalShopItemType.MoneyPocketAbility, PocketMoneyAbility},
            };
        }
    }
    
    [Serializable]
    public class TowerData
    {
        public GlobalShopItemType TowerType;
        public bool IsBought;
        public bool IsUpgradedBought;
        public bool IsOpenToBuy;
    }
    
    [Serializable]
    public class AbilityData
    {
        public GlobalShopItemType AbilityType;
        public bool IsBought;
        public int Count;
        public bool IsOpenToBuy;
    }

    [Serializable]
    public class LevelRow
    {
        public int[] Cols;
    }
}
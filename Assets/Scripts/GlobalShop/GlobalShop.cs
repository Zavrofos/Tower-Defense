using System;
using System.Collections.Generic;
using SaveSystemDir;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GlobalShop
{
    public enum GlobalShopItemType
    {
        TowerLow,
        TowerMedium,
        TowerHigh,
        TowerCold,
        TowerLaser,
        AbilityRocket,
        AbilityMine,
        FoodAbility,
        MoneyPocketAbility,
        MeteorShowerAbility,
        TowerLaserNew
    }
    
    public class GlobalShop : MonoBehaviour
    {
        [field: SerializeField] public ToggleGroup ToggleGroup { get; private set; }
        [field: SerializeField] public Button CloseButton { get; private set; }
        [field: SerializeField] public ItemInGlobalShop ItemInGlobalShopPrefab { get; private set; }
        [field: SerializeField] public Transform Content { get; private set; }
        [field: SerializeField] public DescriptionItem DescriptionItem { get; private set; }
        [field: SerializeField] public GlobalShopConfig GlobalShopConfig { get; private set; }
        [field: SerializeField] public TMP_Text GlobalCoinCount { get; private set; }

        public Dictionary<GlobalShopItemType, ItemInGlobalShop> Items;

        private void OnEnable()
        {
            Items = new Dictionary<GlobalShopItemType, ItemInGlobalShop>();
            
            foreach (GlobalShopItemInfo info in GlobalShopConfig.GlobalShopItemsInfos)
            {
                ItemInGlobalShop item = Instantiate(ItemInGlobalShopPrefab, Content);
                item.Init(info, ToggleGroup);
                Items.TryAdd(info.Type, item);
                
                item.Toggle.onValueChanged.AddListener(value =>
                {
                    if (value) SetDescription(info);
                });
                
                item.BuyButton.onClick.AddListener(() => BuyItem(item));
            }
            
            CloseButton.onClick.AddListener(CloseWindow);
            SetSavedData();
            
            Items[GlobalShopItemType.TowerLow].Toggle.isOn = true;
            EventSystem.current.SetSelectedGameObject(Items[GlobalShopItemType.TowerLow].Toggle.gameObject);
        }

        private void OnDisable()
        {
            List<ItemInGlobalShop> itemsToRemove = new List<ItemInGlobalShop>();
            
            foreach (var itemInGlobalShop in Items.Values)
            {
                if(!itemInGlobalShop)
                    continue;
                
                itemInGlobalShop.Toggle.onValueChanged.RemoveAllListeners();
                itemInGlobalShop.BuyButton.onClick.RemoveAllListeners();
                itemsToRemove.Add(itemInGlobalShop);
            }
            
            for (int i = 0; i < itemsToRemove.Count; i++)
                Destroy(itemsToRemove[i].gameObject);
            
            CloseButton.onClick.RemoveAllListeners();
        }

        private void SetDescription(GlobalShopItemInfo info)
        {
            CurrentGameData currentGameData = SaveSystem.CurrentGameData;
            
            DescriptionItem.Icon.sprite = 
                info.IsUpgradeType && currentGameData.TowersData[info.Type].IsBought
                    ? info.UpgradeDescriptionIcon 
                    : info.IconDescriptionItem;
            
            DescriptionItem.Description.text = 
                info.IsUpgradeType && currentGameData.TowersData[info.Type].IsBought
                    ? info.UpgradedDescription 
                    : info.DescriptionItem;
            
            DescriptionItem.Name.text = 
                info.IsUpgradeType && currentGameData.TowersData[info.Type].IsBought
                    ? info.UpgradedName 
                    : info.NameItem;
        }
        
        private void BuyItem(ItemInGlobalShop item)
        {
            int money = SaveSystem.CurrentGameData.CurrentGlobalMoney;
            int price = int.Parse(item.PriceText.text);
        
            if (money < price)
            {
                Debug.Log("Dont have money!!!");
                return;
            }
        
            bool isTower = item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerLow ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerMedium ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerHigh ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerCold ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerLaser;
        
            if (isTower)
                BuyOrBuyUpgradeTower(item);
            else
                BuyAbility(item);
        
            SaveSystem.CurrentGameData.CurrentGlobalMoney -= price;
            GlobalCoinCount.text = SaveSystem.CurrentGameData.CurrentGlobalMoney.ToString();
            SaveSystem.SaveGame();
        
            item.Toggle.isOn = true;
            EventSystem.current.SetSelectedGameObject(item.Toggle.gameObject);
        }
        
        private void BuyOrBuyUpgradeTower(ItemInGlobalShop item)
        {
            TowerData towerData = SaveSystem.CurrentGameData.TowersData[item.GlobalShopItemInfo.Type];
        
            if (towerData.IsBought)
            {
                towerData.IsUpgradedBought = true;
                item.BuyButton.interactable = false;
                item.BuyButtonText.text = "Bought";
            }
            else
            {
                towerData.IsBought = true;
                item.ItemIcon.sprite = item.GlobalShopItemInfo.UpgradeIcon;
                item.PriceText.text = item.GlobalShopItemInfo.Price.ToString();
            }
        }

        private void BuyAbility(ItemInGlobalShop item)
        {
            SaveSystem.CurrentGameData.AbilityData[item.GlobalShopItemInfo.Type].IsBought = true;
            SaveSystem.CurrentGameData.AbilityData[item.GlobalShopItemInfo.Type].Count++;
            item.CountText.text = $"x{SaveSystem.CurrentGameData.AbilityData[item.GlobalShopItemInfo.Type].Count.ToString()}";

            if (item.GlobalShopItemInfo.Type == GlobalShopItemType.AbilityRocket)
            {
                item.BuyButton.interactable = false;
                item.BuyButtonText.text = "Bought";
            }
        }

        private void CloseWindow()
        {
            gameObject.SetActive(false);
        }
        
        private void SetSavedData()
        {
            GlobalCoinCount.text = SaveSystem.CurrentGameData.CurrentGlobalMoney.ToString();
            SetTowerSaveData(GlobalShopItemType.TowerLow);
            SetTowerSaveData(GlobalShopItemType.TowerMedium);
            SetTowerSaveData(GlobalShopItemType.TowerHigh);
            SetTowerSaveData(GlobalShopItemType.TowerCold);
            SetTowerSaveData(GlobalShopItemType.TowerLaser);
        
            if (SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityRocket].IsBought)
            {
                Items[GlobalShopItemType.AbilityRocket].BuyButton.interactable = false;
                Items[GlobalShopItemType.AbilityRocket].BuyButtonText.text = "Bought";
            }
        }

        private void SetTowerSaveData(GlobalShopItemType type)
        {
            if (Items.ContainsKey(type) && SaveSystem.CurrentGameData.TowersData[type].IsBought)
            {
                ItemInGlobalShop item = Items[type];
                GlobalShopItemInfo info = item.GlobalShopItemInfo;
                item.ItemIcon.sprite = info.UpgradeIcon;
                item.PriceText.text = info.UpgradePrice.ToString();

                if (SaveSystem.CurrentGameData.TowersData[type].IsUpgradedBought)
                {
                    item.BuyButton.interactable = false;
                    item.BuyButtonText.text = "Bought";
                }
            }
        }
    }
}
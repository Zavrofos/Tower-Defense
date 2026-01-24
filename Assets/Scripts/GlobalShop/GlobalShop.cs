using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
        ResetLevelCoin
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

        private void Awake()
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
        }

        private void SetSavedData()
        {
            CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
            GlobalCoinCount.text = currentGameData.CurrentGlobalMoney.ToString();
            SetTowerSaveData(GlobalShopItemType.TowerLow, currentGameData);
            SetTowerSaveData(GlobalShopItemType.TowerMedium, currentGameData);
            SetTowerSaveData(GlobalShopItemType.TowerHigh, currentGameData);
            SetTowerSaveData(GlobalShopItemType.TowerCold, currentGameData);
            SetTowerSaveData(GlobalShopItemType.TowerLaser, currentGameData);

            if (currentGameData.IsRocketAbilityBought)
            {
                Items[GlobalShopItemType.AbilityRocket].BuyButton.interactable = false;
                Items[GlobalShopItemType.AbilityRocket].BuyButtonText.text = "Bought";
            }
        }

        public void SetTowerSaveData(GlobalShopItemType type, CurrentGameData currentGameData)
        {
            Debug.Log($"SetTowerSaveData, items - {Items != null}, currentData - {currentGameData != null}");
            
            if (Items.ContainsKey(type) && currentGameData.TowersData[type].IsBought)
            {
                if (currentGameData.TowersData[type].IsBought)
                {
                    ItemInGlobalShop item = Items[type];
                    GlobalShopItemInfo info = item.GlobalShopItemInfo;
                    item.ItemIcon.sprite = info.UpgradeIcon;
                    item.PriceText.text = info.UpgradePrice.ToString();

                    if (currentGameData.TowersData[type].IsUpgradedBought)
                    {
                        item.BuyButton.interactable = false;
                        item.BuyButtonText.text = "Bought";
                    }
                }
            }
        }

        public void SetDescription(GlobalShopItemInfo info)
        {
            CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
            
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
            Debug.Log("BuyItem 1");
            CurrentGameData currentGameData = GameManager.Instance.CurrentGameData;
            int money = currentGameData.CurrentGlobalMoney;
            int price = int.Parse(item.PriceText.text);

            if (money < price)
            {
                Debug.Log("BuyItem 2");
                Debug.Log("Dont have money!!!");
                return;
            }

            bool isTower = item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerLow ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerMedium ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerHigh ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerCold ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerLaser;

            if (isTower)
            {
                Debug.Log("BuyItem 3");
                BuyOrBuyUpgradeTower(item, currentGameData);
            }
            else if(item.GlobalShopItemInfo.Type == GlobalShopItemType.AbilityMine)
            {
                Debug.Log("BuyItem 4");
                currentGameData.CountMineBought++;
            }
            else if(item.GlobalShopItemInfo.Type == GlobalShopItemType.AbilityRocket)
            {
                Debug.Log("BuyItem 5");
                currentGameData.IsRocketAbilityBought = true;
                item.BuyButton.interactable = false;
                item.BuyButtonText.text = "Bought";
            }
            else if (item.GlobalShopItemInfo.Type == GlobalShopItemType.ResetLevelCoin)
            {
                Debug.Log("BuyItem 6");
                currentGameData.CountResetLevelCoins++;
            }

            currentGameData.CurrentGlobalMoney -= price;
            GlobalCoinCount.text = currentGameData.CurrentGlobalMoney.ToString();
            SaveSystem.SaveSystem.SaveGame();
        }

        private void BuyOrBuyUpgradeTower(ItemInGlobalShop item, CurrentGameData currentGameData)
        {
            Debug.Log($"BuyOrBuyUpgradeTower 1, type - {item.GlobalShopItemInfo.Type}");
            TowerData towerData = currentGameData.TowersData[item.GlobalShopItemInfo.Type];

            if (towerData.IsBought)
            {
                Debug.Log("BuyOrBuyUpgradeTower 2");
                towerData.IsUpgradedBought = true;
                item.BuyButton.interactable = false;
                item.BuyButtonText.text = "Bought";
            }
            else
            {
                Debug.Log("BuyOrBuyUpgradeTower 3");
                towerData.IsBought = true;
                item.ItemIcon.sprite = item.GlobalShopItemInfo.UpgradeIcon;
                item.PriceText.text = item.GlobalShopItemInfo.Price.ToString();
            }
        }

        public void CloseWindow()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            foreach (var itemInGlobalShop in Items.Values)
            {
                if(!itemInGlobalShop)
                    continue;
                
                itemInGlobalShop.Toggle.onValueChanged.RemoveAllListeners();
                itemInGlobalShop.BuyButton.onClick.RemoveAllListeners();
            }
            
            CloseButton.onClick.RemoveAllListeners();
        }
    }
}
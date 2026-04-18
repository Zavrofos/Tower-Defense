using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.UIScripts;
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
        TowerLaserNew,
        None
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
        [field: SerializeField] public Button BuyButton { get; private set; }
        [field: SerializeField] public TMP_Text BuyButtonText { get; private set; }
        
        public Dictionary<GlobalShopItemType, ItemInGlobalShop> Items;

        private void Awake()
        {
            CloseButton.onClick.AddListener(CloseWindow);
        }

        private void OnEnable()
        {
            Items = new Dictionary<GlobalShopItemType, ItemInGlobalShop>();
            
            foreach (GlobalShopItemInfo info in GlobalShopConfig.GlobalShopItemsInfos)
            {
                if(SaveSystem.CurrentGameData.TowersData.ContainsKey(info.Type) && !SaveSystem.CurrentGameData.TowersData[info.Type].IsOpenToBuy ||
                   SaveSystem.CurrentGameData.AbilityData.ContainsKey(info.Type) && !SaveSystem.CurrentGameData.AbilityData[info.Type].IsOpenToBuy)
                    continue;
                
                ItemInGlobalShop item = Instantiate(ItemInGlobalShopPrefab, Content);
                item.Init(info, ToggleGroup);
                Items.TryAdd(info.Type, item);
                
                item.Toggle.onValueChanged.AddListener(value =>
                {
                    if (value)
                    {
                        SetDescription(info);
                        BuyButton.onClick.RemoveAllListeners();
                        BuyButton.onClick.AddListener(() => BuyItem(item, info));
                        BuyButton.interactable = !item.Bought;
                        BuyButtonText.text = item.Bought ? "Bought" : "Buy";
                        item.PriceText.gameObject.SetActive(!item.Bought);
                        item.CoinIcon.gameObject.SetActive(!item.Bought);
                        item.BoughtImage.gameObject.SetActive(item.Bought);
                    }
                });
            }
            
            SetSavedData();
            
            Items[GlobalShopItemType.TowerLow].Toggle.isOn = true;
            EventSystem.current.SetSelectedGameObject(Items[GlobalShopItemType.TowerLow].Toggle.gameObject);

            foreach (var itemsValue in Items.Values)
            {
                itemsValue.Toggle.onValueChanged.AddListener(value =>
                {
                    ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.CLickAudioUI);
                });
            }
        }

        private void OnDisable()
        {
            List<ItemInGlobalShop> itemsToRemove = new List<ItemInGlobalShop>();
            
            foreach (var itemInGlobalShop in Items.Values)
            {
                if(!itemInGlobalShop)
                    continue;
                
                itemsToRemove.Add(itemInGlobalShop);
            }
            
            for (int i = 0; i < itemsToRemove.Count; i++)
                Destroy(itemsToRemove[i].gameObject);
        }

        private void OnDestroy()
        {
            CloseButton.onClick.RemoveAllListeners();
        }

        private void SetDescription(GlobalShopItemInfo info)
        {
            CurrentGameData currentGameData = SaveSystem.CurrentGameData;
            
            DescriptionItem.Icon.sprite = 
                info.IsUpgradeType && currentGameData.TowersData[info.Type].IsBought
                    ? info.UpgradeDescriptionIcon 
                    : info.IconDescriptionItem;
            
            DescriptionItem.Description.Key = 
                info.IsUpgradeType && currentGameData.TowersData[info.Type].IsBought
                    ? info.UpgradedDescriptionKey 
                    : info.DescriptionItemKey;
            
            DescriptionItem.Description.SetLocalization();
            
            DescriptionItem.Name.Key = 
                info.IsUpgradeType && currentGameData.TowersData[info.Type].IsBought
                    ? info.UpgradedNameKey 
                    : info.NameItemKey;
            
            DescriptionItem.Name.SetLocalization();
        }
        
        private void BuyItem(ItemInGlobalShop item, GlobalShopItemInfo info)
        {
            int money = SaveSystem.CurrentGameData.CurrentGlobalMoney;
            int price = int.Parse(item.PriceText.text);
        
            if (money < price)
            {
                Debug.Log("Dont have money!!!");
                return;
            }
        
            ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.MoneyAudio);
            
            bool isTower = item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerLow ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerMedium ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerHigh ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerCold ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerLaser ||
                           item.GlobalShopItemInfo.Type == GlobalShopItemType.TowerLaserNew;
        
            if (isTower)
                BuyOrBuyUpgradeTower(item);
            else
                BuyAbility(item);
        
            SetDescription(info);
            
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
                BuyButton.interactable = false;
                BuyButtonText.text = "Bought";
                item.Bought = true;
                item.PriceText.gameObject.SetActive(false);
                item.CoinIcon.gameObject.SetActive(false);
                item.BoughtImage.gameObject.SetActive(true);
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
                BuyButton.interactable = false;
                BuyButtonText.text = "Bought";
                item.Bought = true;
                item.PriceText.gameObject.SetActive(false);
                item.CoinIcon.gameObject.SetActive(false);
                item.BoughtImage.gameObject.SetActive(true);
            }
        }

        private void CloseWindow()
        {
            ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.BackAudioUI);
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
                Items[GlobalShopItemType.AbilityRocket].Bought = true;
                Items[GlobalShopItemType.AbilityRocket].PriceText.gameObject.SetActive(false);
                Items[GlobalShopItemType.AbilityRocket].CoinIcon.gameObject.SetActive(false);
                Items[GlobalShopItemType.AbilityRocket].BoughtImage.gameObject.SetActive(true);
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
                    item.Bought = true;
                    item.PriceText.gameObject.SetActive(false);
                    item.CoinIcon.gameObject.SetActive(false);
                    item.BoughtImage.gameObject.SetActive(true);
                }
            }
        }
    }
}
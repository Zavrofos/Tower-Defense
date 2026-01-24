using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GlobalShop
{
    public enum GlobalShopItemType
    {
        TowerLow,
        TowerLowUpgrade,
        TowerMedium,
        TowerMediumUpgrade,
        TowerHigh,
        TowerHighUpgrade,
        TowerCold,
        TowerColdUpgrade,
        TowerLaser,
        TowerLaserUpgrade,
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
                
                item.BuyButton.onClick.AddListener(BuyItem);
            }
            
            CloseButton.onClick.AddListener(CloseWindow);
        }

        public void SetDescription(GlobalShopItemInfo info)
        {
            DescriptionItem.Icon.sprite = info.IconDescriptionItem;
            DescriptionItem.Description.text = info.DescriptionItem;
            DescriptionItem.Name.text = info.NameItem;
        }

        public void BuyItem()
        {
            Debug.Log("Buy item!!!");
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
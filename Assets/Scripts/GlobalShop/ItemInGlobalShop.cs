using System;
using SaveSystemDir;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GlobalShop
{
    public class ItemInGlobalShop : MonoBehaviour
    {
        [field: SerializeField] public Toggle Toggle { get; private set; }
        [field: SerializeField] public Image ItemIcon { get; private set; }
        [field: SerializeField] public Image BoughtImage { private set; get; }
        [field: SerializeField] public Image CoinIcon { private set; get; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public GlobalShopItemInfo GlobalShopItemInfo { get; private set; }
        [field: SerializeField] public TMP_Text CountText;
        [field: SerializeField] public bool Bought { get; set; }

        public void Init(GlobalShopItemInfo globalShopItemInfo, ToggleGroup toggleGroup)
        {
            Toggle.group = toggleGroup;
            ItemIcon.sprite = globalShopItemInfo.IconShopItem;
            PriceText.text = globalShopItemInfo.Price.ToString();
            GlobalShopItemInfo = globalShopItemInfo;

            if (globalShopItemInfo.Type is 
                GlobalShopItemType.AbilityMine or 
                GlobalShopItemType.MoneyPocketAbility or 
                GlobalShopItemType.FoodAbility or 
                GlobalShopItemType.MeteorShowerAbility)
            {
                int count = SaveSystem.CurrentGameData.AbilityData[globalShopItemInfo.Type].Count;
                CountText.text = $"x{count}";
                CountText.gameObject.SetActive(true);
            }
            else if (globalShopItemInfo.Type is not GlobalShopItemType.AbilityRocket)
            {
                TowerData towerData = SaveSystem.CurrentGameData.TowersData[globalShopItemInfo.Type];

                if (towerData.IsBought)
                {
                    ItemIcon.sprite = globalShopItemInfo.UpgradeIcon;
                    PriceText.text = globalShopItemInfo.UpgradePrice.ToString();
                }
            }
        }

        private void OnDestroy()
        {
            Toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
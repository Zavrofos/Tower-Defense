using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GlobalShop
{
    public class ItemInGlobalShop : MonoBehaviour
    {
        [field: SerializeField] public Toggle Toggle { get; private set; }
        [field: SerializeField] public Image ItemIcon { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public Button BuyButton { get; private set; }
        [field: SerializeField] public TMP_Text BuyButtonText { get; private set; }
        [field: SerializeField] public GlobalShopItemInfo GlobalShopItemInfo { get; private set; }
        [field: SerializeField] public TMP_Text CountText;

        public void Init(GlobalShopItemInfo globalShopItemInfo, ToggleGroup toggleGroup)
        {
            Toggle.group = toggleGroup;
            ItemIcon.sprite = globalShopItemInfo.IconShopItem;
            PriceText.text = globalShopItemInfo.Price.ToString();
            GlobalShopItemInfo = globalShopItemInfo;

            if (globalShopItemInfo.Type is GlobalShopItemType.AbilityMine or GlobalShopItemType.ResetLevelCoin)
            {
                int count = globalShopItemInfo.Type == GlobalShopItemType.AbilityMine
                    ? GameManager.Instance.CurrentGameData.CountMineBought
                    : GameManager.Instance.CurrentGameData.CountResetLevelCoins;

                CountText.text = $"x{count}";
                CountText.gameObject.SetActive(true);
            }
        }
    }
}
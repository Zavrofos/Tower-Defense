using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class WinMenuNewItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private LocalizationText _name;
        [SerializeField] private Image Image;
        [SerializeField] private NewItemDescription _newItemDescription;

        private GlobalShopConfig _config;
        private GlobalShopItemType _itemType;
        
        public void Setup(GlobalShopItemType itemType)
        {
            _config = GameManager.Instance.GameHub.ShopWindow.GlobalShopConfig;
            _itemType = itemType;
            _name.Key = _config.InfosDic[itemType].NameItemKey;
            _name.SetLocalization();
            Image.sprite = _config.InfosDic[itemType].IconShopItem;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _newItemDescription.Name.Key = _config.InfosDic[_itemType].NameItemKey;
            _newItemDescription.Name.SetLocalization();
            _newItemDescription.ImageDescription.sprite = _config.InfosDic[_itemType].IconDescriptionItem;
            _newItemDescription.DescriptionText.Key = _config.InfosDic[_itemType].DescriptionItemKey;
            _newItemDescription.DescriptionText.SetLocalization();
            _newItemDescription.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _newItemDescription.gameObject.SetActive(false);
        }
    }
}
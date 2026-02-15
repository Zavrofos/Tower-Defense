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
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Image Image;
        [SerializeField] private NewItemDescription _newItemDescription;

        private GlobalShopConfig _config;
        private GlobalShopItemType _itemType;
        
        public void Setup(GlobalShopItemType itemType)
        {
            _config = GameManager.Instance.GameHub.ShopWindow.GlobalShopConfig;
            _itemType = itemType;
            _name.text = _config.InfosDic[itemType].NameItem;
            Image.sprite = _config.InfosDic[itemType].IconShopItem;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _newItemDescription.Name.text = _config.InfosDic[_itemType].NameItem;
            _newItemDescription.ImageDescription.sprite = _config.InfosDic[_itemType].IconDescriptionItem;
            _newItemDescription.DescriptionText.text = _config.InfosDic[_itemType].DescriptionItem;
            _newItemDescription.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _newItemDescription.gameObject.SetActive(false);
        }
    }
}
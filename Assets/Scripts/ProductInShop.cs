using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using Assets.Scripts.UIScripts;
using GameOverlayWindow;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductInShop : MonoBehaviour
{
    public Image ImageProduct;
    public RectTransform ImageRectTransform;
    public TMP_Text LabelProduct;
    public Button ButtonBuy;
    public LocalizationText TextBuyBatton;
    public AbsTower Tower;
    public BuildingPoint BuildingPoint;
    public TMP_Text PriceText;
    public TMP_Text DescriptionText;
    
    public Shop Shop { get; set; }

    private void Awake()
    {
        BuildingPoint = GetComponentInParent<Shop>().BuildingPoint;
    }
    private void OnEnable()
    {
        ButtonBuy.onClick.AddListener(OnBuy);

        if (!BuildingPoint.CurrentTower || BuildingPoint.CurrentTower.Type != Tower.Type)
        {
            TextBuyBatton.Key = "Buy";
            TextBuyBatton.SetLocalization();
        }
    }

    private void OnDisable()
    {
        ButtonBuy.onClick.RemoveListener(OnBuy);
    }

    private void OnBuy()
    {
        if(TextBuyBatton.Key != "Buyed")
        {
            GameOverlay gameOverlay = GameManager.Instance.GameOverlay;
            
            if (int.Parse(PriceText.text) > int.Parse(gameOverlay.CoinsText.text)) 
                return;
            
            ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.MoneyAudio);
            gameOverlay.CoinsText.text = (int.Parse(gameOverlay.CoinsText.text) - int.Parse(PriceText.text)).ToString();
            BuildingPoint.BuildingTower(Tower);
            TextBuyBatton.Key = "Buyed";
            TextBuyBatton.SetLocalization();

            foreach (var productInShop in Shop.Products)
                if (productInShop.Tower.Type != Tower.Type)
                {
                    productInShop.TextBuyBatton.Key = "Buy";
                    productInShop.TextBuyBatton.SetLocalization();
                }
            
            Shop.Close();
        }
    }
}

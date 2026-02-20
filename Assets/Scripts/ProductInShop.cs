using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
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
    public TMP_Text ButtonText;
    public GameObject Tower;
    public BuildingPoint BuildingPoint;
    public TMP_Text PriceText;
    public TMP_Text DescriptionText;
    private GameManagerInGame _gameManager;

    private void Start()
    {
        BuildingPoint = GetComponentInParent<Shop>().BuildingPoint;
        _gameManager = GameManager.Instance.CurrentGameManagerLevel;
    }
    private void OnEnable()
    {
        ButtonBuy.onClick.AddListener(OnBuy);
    }

    private void OnDisable()
    {
        ButtonBuy.onClick.RemoveListener(OnBuy);
    }

    public void OnBuy()
    {
        if(ButtonText.text != "Buyed")
        {
            GameOverlay gameOverlay = GameManager.Instance.GameOverlay;
            
            if (int.Parse(PriceText.text) > int.Parse(gameOverlay.CoinsText.text)) 
                return;
            
            ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.MoneyAudio);
            gameOverlay.CoinsText.text = (int.Parse(gameOverlay.CoinsText.text) - int.Parse(PriceText.text)).ToString();
            BuildingPoint.BuildingTower(Tower);
            ButtonText.text = "Buyed";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
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
            if (int.Parse(PriceText.text) > _gameManager.Coins) return;
            _gameManager.SpendCoins(int.Parse(PriceText.text));
            BuildingPoint.BuildingTower(Tower);
            ButtonText.text = "Buyed";
        }
    }
}

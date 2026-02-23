using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using SaveSystemDir;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _conteiner;
    [SerializeField] private ProductInShop _productPrefab;
    public BuildingPoint BuildingPoint;

    public List<ProductInShop> Products { get; private set; } = new List<ProductInShop>();

    private void Start()
    {
        List<GameObject> towers = new List<GameObject>();
        CurrentGameData currentGameData = SaveSystem.CurrentGameData;

        foreach (TowerInfo towerInfo in GameManager.Instance.GameAssets.TowersInfos)
            if (currentGameData.TowersData[towerInfo.Type].IsBought)
                towers.Add(towerInfo.TowerPrefab);
            
        foreach(var tower in towers)
            InstantiateTower(tower);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void InstantiateTower(GameObject towerObject)
    {
        ProductInShop product = Instantiate(_productPrefab, _conteiner.transform);
        AbsTower tower = towerObject.GetComponent<AbsTower>();
        if (tower == null) return;
        product.ImageProduct.sprite = tower.Icon;
        product.ImageProduct.preserveAspect = true;
        product.ImageProduct.SetNativeSize();

        RectTransform transform = product.ImageProduct.gameObject.GetComponent<RectTransform>();
        Vector2 size = transform.sizeDelta;
        Vector2 newSize = new Vector2(size.x * 1.5f, size.y * 1.5f);
        transform.sizeDelta = newSize;

        product.LabelProduct.text = tower.Label;
        product.Tower = tower;
        product.PriceText.text = tower.Price.ToString();
        product.DescriptionText.text = tower.Description;
        product.Shop = this;
        Products.Add(product);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<GameObject> _towers;
    [SerializeField] private GameObject _conteiner;
    [SerializeField] private ProductInShop _productPrefab;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Slider _slider;
    public BuildingPoint BuildingPoint;

    private void Start()
    {
        foreach(var tower in _towers)
        {
            InstantiateTower(tower);
        }
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
        product.ImageRectTransform.rotation = Quaternion.Euler(tower.InitRotationImageInShop);
        product.ImageProduct.SetNativeSize();

        RectTransform transform = product.ImageProduct.gameObject.GetComponent<RectTransform>();
        Vector2 size = transform.sizeDelta;
        Vector2 newSize = new Vector2(size.x * 1.5f, size.y * 1.5f);
        transform.sizeDelta = newSize;
        

        product.LabelProduct.text = tower.Label;
        product.Tower = towerObject;
        product.PriceText.text = tower.Price.ToString();
        product.DescriptionText.text = tower.Description;
    }
}

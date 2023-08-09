using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementButton : MonoBehaviour
{
    [SerializeField] private BuildingPoint _buildingPoint;
    public TMP_Text UpgradePriceText;
    [SerializeField] private GameObject _improvementPriceObj;

    private void OnEnable()
    {
        _improvementPriceObj.SetActive(true);
    }

    private void Start()
    {
        _improvementPriceObj.SetActive(true);
        UpgradePriceText.text = _buildingPoint.CurrentTower.GetComponent<Tower>().UpgradePrice.ToString();
    }

    private void OnMouseDown()
    {
        GameManagerInGame gameManager = FindObjectOfType<GameManagerInGame>();
        if (gameManager.IsDisableButtonColliders) return;
        if(gameManager.Coins < int.Parse(UpgradePriceText.text))
        {
            return;
        }
        Tower tower = _buildingPoint.CurrentTower.GetComponent<Tower>();
        gameManager.SpendCoins(tower.UpgradePrice);
        tower.Improve();
        _improvementPriceObj.SetActive(false);
        gameObject.SetActive(false);
    }
}

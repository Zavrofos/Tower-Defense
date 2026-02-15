using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using GameOverlayWindow;
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
        UpgradePriceText.text = _buildingPoint.CurrentTower.GetComponent<AbsTower>().UpgradePrice.ToString();
    }

    private void OnMouseDown()
    {
        GameManagerInGame gameManager = GameManager.Instance.CurrentGameManagerLevel;
        GameOverlay gameOverlay = GameManager.Instance.GameOverlay;
        
        if (gameManager.IsDisableButtonColliders) return;
        
        if(int.Parse(gameOverlay.CoinsText.text) < int.Parse(UpgradePriceText.text))
            return;
        
        AbsTower tower = _buildingPoint.CurrentTower.GetComponent<AbsTower>();
        gameOverlay.CoinsText.text = (int.Parse(gameOverlay.CoinsText.text) - tower.UpgradePrice).ToString();
        tower.Improve();
        _improvementPriceObj.SetActive(false);
        gameObject.SetActive(false);
    }
}

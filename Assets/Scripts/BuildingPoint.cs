using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPoint : MonoBehaviour
{
    public GameObject CurrentTower;
    public ImprovementButton ButtonImprovement;
    [SerializeField] private GameManagerInGame _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManagerInGame>();
    }
    public void BuildingTower(GameObject tower)
    {
        if(CurrentTower == null)
        {
            CurrentTower = Instantiate(tower, gameObject.transform);
            
        }
        else
        {
            Destroy(CurrentTower);
            CurrentTower = Instantiate(tower, gameObject.transform);
            ButtonImprovement.UpgradePriceText.text = tower.GetComponent<Tower>().UpgradePrice.ToString();
        }
        ButtonImprovement.gameObject.SetActive(true);
    }
}

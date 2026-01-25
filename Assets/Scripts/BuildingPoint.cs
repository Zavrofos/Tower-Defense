using Assets.Scripts;
using UnityEngine;

public class BuildingPoint : MonoBehaviour
{
    public GameObject CurrentTower;
    public ImprovementButton ButtonImprovement;

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
            
        }
        
        AbsTower absTower = CurrentTower.GetComponent<AbsTower>();
        ButtonImprovement.UpgradePriceText.text = absTower.UpgradePrice.ToString();
        bool isUpgradedBought = GameManager.Instance.CurrentGameData.TowersData[absTower.Type].IsUpgradedBought;
        ButtonImprovement.gameObject.SetActive(isUpgradedBought);
    }
}

using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using SaveSystemDir;
using UnityEngine;

public class BuildingPoint : MonoBehaviour
{
    public GameObject CurrentTower { get; private set; }
    public ImprovementButton ButtonImprovement;
    public AudioClip _buildTowerAudio;

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
        
        SoundBox soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, transform.position, transform.rotation);
        soundBox.Play(_buildTowerAudio, false);
        
        AbsTower absTower = CurrentTower.GetComponent<AbsTower>();
        ButtonImprovement.UpgradePriceText.text = absTower.UpgradePrice.ToString();
        bool isUpgradedBought = SaveSystem.CurrentGameData.TowersData[absTower.Type].IsUpgradedBought;
        ButtonImprovement.gameObject.SetActive(isUpgradedBought);
    }
}

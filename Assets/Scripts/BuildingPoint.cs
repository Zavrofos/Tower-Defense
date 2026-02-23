using Assets.Scripts;
using Assets.Scripts.RepPoolObject;
using SaveSystemDir;
using UnityEngine;

public class BuildingPoint : MonoBehaviour
{
    public AbsTower CurrentTower { get; private set; }
    public ImprovementButton ButtonImprovement;
    public AudioClip _buildTowerAudio;

    public void BuildingTower(AbsTower tower)
    {
        if(CurrentTower)
            Destroy(CurrentTower.gameObject);
        
        CurrentTower = Instantiate(tower, gameObject.transform);
        
        SoundBox soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, transform.position, transform.rotation);
        soundBox.Play(_buildTowerAudio, false);
        
        ButtonImprovement.UpgradePriceText.text = CurrentTower.UpgradePrice.ToString();
        bool isUpgradedBought = SaveSystem.CurrentGameData.TowersData[CurrentTower.Type].IsUpgradedBought;
        ButtonImprovement.gameObject.SetActive(isUpgradedBought);
    }
}

using System;
using Assets.Scripts.GlobalShop;
using SaveSystemDir;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class GameOverlay : MonoBehaviour
    { 
        [field: SerializeField] public Button PauseGameButton { get; private set; }
        [field: SerializeField] public Button SetGameFasterButton { get; private set; }
        [field: SerializeField] public Button SetNextWaveButton { get; private set; }
        
        [field: SerializeField] public HealthBar HealthBar { get; private set; }
        [field: SerializeField] public TMP_Text CoinsText { get; private set; }
        [field: SerializeField] public AbilityRocketButton AbilityRocketButton { get; private set; }
        [field: SerializeField] public AbilityMineButton AbilityMineButton { get; private set; }
        [field: SerializeField] public AbilityMeteorButton AbilityMeteorButton { get; private set; }
        [field: SerializeField] public AbilityFoodButton AbilityFoodButton { get; private set; }
        [field: SerializeField] public AbilityPocketMoneyButton AbilityPocketMoneyButton { get; private set; }

        private void OnEnable()
        {
            AbilityRocketButton.gameObject.SetActive(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityRocket].IsBought);
            
            AbilityMineButton.gameObject.SetActive(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].IsBought);
            AbilityMineButton.CountText.text = SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].Count.ToString();
            AbilityMineButton.Button.interactable = SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].Count > 0;
            
            AbilityMeteorButton.gameObject.SetActive(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MeteorShowerAbility].IsBought);
            AbilityMeteorButton.SetCountText(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MeteorShowerAbility].Count.ToString());
            AbilityMeteorButton.SetInteractableButton(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MeteorShowerAbility].Count > 0);
            
            AbilityFoodButton.gameObject.SetActive(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.FoodAbility].IsBought);
            AbilityFoodButton.SetInteractableButton(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.FoodAbility].Count > 0);
            AbilityFoodButton.SetCountText(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.FoodAbility].Count.ToString());
            
            AbilityPocketMoneyButton.gameObject.SetActive(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MoneyPocketAbility].IsBought);
            AbilityPocketMoneyButton.SetInteractableButton(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MoneyPocketAbility].Count > 0);
            AbilityPocketMoneyButton.SetCountText(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MoneyPocketAbility].Count.ToString());
        }
    }
}
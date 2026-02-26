using System;
using Assets.Scripts.GlobalShop;
using SaveSystemDir;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class AbilityMineButton : MonoBehaviour
    {
        [SerializeField] private MineAbility _mineAbilityPrefab;
        [field: SerializeField] public Button Button { get; private set; }
        public TMP_Text CountText;
        
        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _disableColor;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _backgroundCount;

        private void Awake()
        {
            Button.onClick.AddListener(TakeAbility);
        }

        private void TakeAbility()
        {
            MineAbility mine = Instantiate(_mineAbilityPrefab);
            mine.GetComponent<Animator>().speed = 0;
            mine.GetComponent<BoxCollider2D>().enabled = false;
            mine.AbilityMineButton = this;
            SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].Count--;
            CountText.text = SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].Count.ToString();
            SetInteractableButton(SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].Count > 0);
        }
        
        public void SetInteractableButton(bool value)
        {
            Button.interactable = value;
            _icon.color = value ? _enableColor : _disableColor;
            _backgroundCount.color = value ? _enableColor : _disableColor;
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(TakeAbility);
        }
    }
}
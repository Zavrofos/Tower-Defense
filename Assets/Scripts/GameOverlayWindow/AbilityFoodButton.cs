using Assets.Scripts;
using SaveSystemDir;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class AbilityFoodButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _backgroundCount;
        [SerializeField] private TMP_Text _count;

        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _disableColor;

        private void Awake()
        {
            gameObject.SetActive(SaveSystem.CurrentGameData.FoodAbilityBought);
            SetInteractableButton(SaveSystem.CurrentGameData.CountFoodBought > 0);
            _count.text = SaveSystem.CurrentGameData.CountFoodBought.ToString();
            _button.onClick.AddListener(UseFood);
        }

        private void SetInteractableButton(bool value)
        {
            _button.interactable = value;
            _icon.color = value ? _enableColor : _disableColor;
            _backgroundCount.color = value ? _enableColor : _disableColor;
        }

        private void UseFood()
        {
            GameManager.Instance.CurrentGameManagerLevel.Home.AddHealth(20);
            SaveSystem.CurrentGameData.CountFoodBought--;
            _count.text = SaveSystem.CurrentGameData.CountFoodBought.ToString();
            
            if(SaveSystem.CurrentGameData.CountFoodBought == 0) 
                SetInteractableButton(false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
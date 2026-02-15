using SaveSystemDir;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class AbilityPocketMoneyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _backgroundCount;
        [SerializeField] private TMP_Text _count;

        [SerializeField] private Color _enableColor;
        [SerializeField] private Color _disableColor;

        private void Awake()
        {
            gameObject.SetActive(SaveSystem.CurrentGameData.MoneyPocketAbilityBought);
            SetInteractableButton(SaveSystem.CurrentGameData.CountMoneyPocketsBought > 0);
            _count.text = SaveSystem.CurrentGameData.CountMoneyPocketsBought.ToString();
            _button.onClick.AddListener(AddMoney);
        }

        private void SetInteractableButton(bool value)
        {
            _button.interactable = value;
            _icon.color = value ? _enableColor : _disableColor;
            _backgroundCount.color = value ? _enableColor : _disableColor;
        }

        private void AddMoney()
        {
            _count.text = (int.Parse(_count.text) + 10).ToString();
            SaveSystem.CurrentGameData.CountMoneyPocketsBought--;
            _count.text = SaveSystem.CurrentGameData.CountMoneyPocketsBought.ToString();
            if (SaveSystem.CurrentGameData.CountMoneyPocketsBought == 0)
                SetInteractableButton(false);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
using System;
using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using Assets.Scripts.RepPoolObject;
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

        [SerializeField] private AudioClip _clickAudio;
        
        private SoundBox _soundBox;

        private void Awake()
        {
            _button.onClick.AddListener(AddMoney);
        }

        public void SetInteractableButton(bool value)
        {
            _button.interactable = value;
            _icon.color = value ? _enableColor : _disableColor;
            _backgroundCount.color = value ? _enableColor : _disableColor;
        }
        
        public void SetCountText(string count)
        {
            _count.text = count;
        }

        private void AddMoney()
        {
            if (_soundBox)
            {
                _soundBox.Stop();
                _soundBox = null;
            }

            _soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
            _soundBox.OnFinished += HandleSoundFinished;
            _soundBox.Play(_clickAudio, false);
            GameManager.Instance.GameOverlay.CoinsText.text = (int.Parse(GameManager.Instance.GameOverlay.CoinsText.text) + 10).ToString();
            SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MoneyPocketAbility].Count--;
            _count.text = SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MoneyPocketAbility].Count.ToString();
            if (SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.MoneyPocketAbility].Count == 0)
                SetInteractableButton(false);
        }
        
        private void HandleSoundFinished(SoundBox box)
        {
            if (_soundBox == box)
                _soundBox = null;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
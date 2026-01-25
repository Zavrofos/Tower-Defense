using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonAbility : MonoBehaviour
    {
        [SerializeField] private Button _abilityButton;
        [SerializeField] private RadiusAbility _radiusAbility;
        [SerializeField] private Ability _abilityPrefab;
        [SerializeField] private GameManagerInGame _gameManager;
        [SerializeField] private TMP_Text _countText;

        [SerializeField] public Image ImageTime;
        [SerializeField] private float _timeToUsing;
        private float _runningTime;
        public bool IsReady = true;
        public AudioSource AudioExplosion;
        
        private void Start()
        {
            _gameManager = FindObjectOfType<GameManagerInGame>();
            if(ImageTime != null) ImageTime.fillAmount = 0;
        }

        public void SetInteractableButton(bool value)
        {
            _abilityButton.interactable = value;
        }

        public void SetCountMine(int count)
        {
            if(_abilityPrefab is AbilityRocket)
                return;

            _countText.text = count.ToString();
        }
        

        private void Update()
        {
            if(!IsReady)
            {
                if(_runningTime < _timeToUsing)
                {
                    _runningTime += Time.deltaTime;
                    ImageTime.fillAmount = 1 - _runningTime / _timeToUsing;
                }
                else
                {
                    IsReady = true;
                    ImageTime.fillAmount = 0;
                    _runningTime = 0;
                }
            }
        }

        private void TakeAbility()
        {
            if(_abilityPrefab is AbilityRocket)
            {
                if(!IsReady)
                {
                    return;
                }
                ImageTime.fillAmount = 1;
            }

            if(_abilityPrefab is AbilityMine)
            {
                if(GameManager.Instance.CurrentGameData.CountMineBought == 0)
                    return;

                GameManager.Instance.CurrentGameData.CountMineBought--;
                _countText.text = GameManager.Instance.CurrentGameData.CountMineBought.ToString();
                _abilityButton.interactable = GameManager.Instance.CurrentGameData.CountMineBought > 0;
                SaveSystem.SaveSystem.SaveGame();
            }
            GameManagerInGame gameManager = FindObjectOfType<GameManagerInGame>();
            gameManager.IsDisableButtonColliders = true;
            RadiusAbility radiusAbility = Instantiate(_radiusAbility);
            radiusAbility.Ability = _abilityPrefab;
            radiusAbility.Transform.localScale = new Vector2(_abilityPrefab.DamageRadius * 2, _abilityPrefab.DamageRadius * 2);
            radiusAbility.ButtonAbility = this;
        }

        private void OnEnable()
        {
            _abilityButton.onClick.AddListener(TakeAbility);
        }

        private void OnDisable()
        {
            _abilityButton.onClick.RemoveListener(TakeAbility);
        }
    }
}
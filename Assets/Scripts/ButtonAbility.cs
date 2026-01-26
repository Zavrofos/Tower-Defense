using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonAbility : MonoBehaviour
    {
        [SerializeField] private RadiusAbility _radiusAbility;
        [SerializeField] private Ability _abilityPrefab;
        
        public Button AbilityButton;
        public TMP_Text CountText;

        [SerializeField] public Image ImageTime;
        [SerializeField] private float _timeToUsing;
        private float _runningTime;
        public bool IsReady = true;
        public AudioSource AudioExplosion;
        
        private void Start()
        {
            if(ImageTime != null) 
                ImageTime.fillAmount = 0;
        }

        public void SetInteractableButton(bool value)
        {
            AbilityButton.interactable = value;
        }

        public void SetCountMine(int count)
        {
            if(_abilityPrefab is AbilityRocket)
                return;

            CountText.text = count.ToString();
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
                
                GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = true;
                RadiusAbility radiusAbility = Instantiate(_radiusAbility);
                radiusAbility.Ability = _abilityPrefab;
                radiusAbility.Transform.localScale = new Vector2(_abilityPrefab.DamageRadius * 2, _abilityPrefab.DamageRadius * 2);
                radiusAbility.ButtonAbility = this;
            }

            if(_abilityPrefab is AbilityMine)
            {
                if(GameManager.Instance.CurrentGameData.CountMineBought == 0)
                    return;

                AbilityMine mine = (AbilityMine) Instantiate(_abilityPrefab);
                mine.GetComponent<Animator>().speed = 0;
                GameManager.Instance.CurrentGameData.CountMineBought--;
                CountText.text = GameManager.Instance.CurrentGameData.CountMineBought.ToString();
                AbilityButton.interactable = GameManager.Instance.CurrentGameData.CountMineBought > 0;
                SaveSystem.SaveSystem.SaveGame();
            }
        }

        private void OnEnable()
        {
            AbilityButton.onClick.AddListener(TakeAbility);
        }

        private void OnDisable()
        {
            AbilityButton.onClick.RemoveListener(TakeAbility);
        }
    }
}
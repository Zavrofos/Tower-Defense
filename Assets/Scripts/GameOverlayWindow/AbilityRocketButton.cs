using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using SaveSystemDir;
using UnityEngine;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class AbilityRocketButton : MonoBehaviour
    {
        [SerializeField] private RadiusAbility _radiusAbility;
        [SerializeField] private RocketAbility _abilityPrefab;
        [SerializeField] private Button _button;
        [SerializeField] public Image ImageTime;
        [SerializeField] private float _timeToUsing;

        public bool IsReady { get; set; } = true;
        private float _runningTime;

        private void Awake()
        {
            _button.onClick.AddListener(TakeAbility);
        }

        private void OnEnable()
        {
            ImageTime.fillAmount = 0;
        }
        
        private void Update()
        {
            if (IsReady) 
                return;
            
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

        private void TakeAbility()
        {
            if (!IsReady)
                return;

            ImageTime.fillAmount = 1;
            GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = true;
            RadiusAbility radiusAbility = Instantiate(_radiusAbility);
            radiusAbility.RocketAbility = _abilityPrefab;
            radiusAbility.transform.localScale = new Vector2(4, 4);
            radiusAbility.ButtonAbility = this;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(TakeAbility);
        }
    }
}
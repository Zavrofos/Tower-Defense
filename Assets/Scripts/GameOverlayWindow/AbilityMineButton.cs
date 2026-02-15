using System;
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

        private void Awake()
        {
            Button.onClick.AddListener(TakeAbility);
            gameObject.SetActive(SaveSystem.CurrentGameData.IsMineAbilityBought);
            CountText.text = SaveSystem.CurrentGameData.CountMineBought.ToString();
            Button.interactable = SaveSystem.CurrentGameData.CountMineBought > 0;
        }
        
        private void TakeAbility()
        {
            MineAbility mine = Instantiate(_mineAbilityPrefab);
            mine.GetComponent<Animator>().speed = 0;
            mine.GetComponent<BoxCollider2D>().enabled = false;
            mine.AbilityMineButton = this;
            SaveSystem.CurrentGameData.CountMineBought--;
            CountText.text = SaveSystem.CurrentGameData.CountMineBought.ToString();
            Button.interactable = SaveSystem.CurrentGameData.CountMineBought > 0;
        }

        private void OnDestroy()
        {
            Button.onClick.RemoveListener(TakeAbility);
        }
    }
}
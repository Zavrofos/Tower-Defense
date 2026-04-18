using System;
using SaveSystemDir;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class LocalizationText : MonoBehaviour
    {
        public string Key;

        private TMP_Text _TMPText;
        
        private void Start()
        {
            TryGetComponent(out _TMPText);
            SetLocalization();
            LocalizationSystem.Instance.OnLanguageChanged += SetLocalization;
        }

        public void SetLocalization()
        {
            if(!_TMPText)
                return;
            
            Language currentLanguage = SaveSystem.GetCurrentLanguage();
            LocalizationBox localizationBox = LocalizationSystem.Instance.LocalizationSo.GetLocalizationBox(Key);
            
            if(localizationBox == null)
                return;

            switch (currentLanguage)
            {
                case Language.Russian:
                    _TMPText.text = localizationBox.RusLocalization;
                    break;
                case Language.English:
                    _TMPText.text = localizationBox.EngLocalization;
                    break;
                case Language.Turkish:
                    _TMPText.text = localizationBox.TurkLocalization;
                    break;
            }
        }
    }
}
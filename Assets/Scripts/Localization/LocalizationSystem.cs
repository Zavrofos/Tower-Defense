using System;
using SaveSystemDir;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class LocalizationSystem : MonoBehaviour
    {
        public static LocalizationSystem Instance;

        [field: SerializeField] public LocalizationSo LocalizationSo;
        
        public event Action OnLanguageChanged;

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LocalizationSo.Init();
        }

        public void SetLanguage(Language language)
        {
            SaveSystem.SaveLocalization(language);
            OnLanguageChanged?.Invoke();
        }
    }
}
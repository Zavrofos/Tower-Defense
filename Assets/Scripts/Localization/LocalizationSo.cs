using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Localization")]
    public class LocalizationSo : ScriptableObject
    {
        public List<LocalizationBox> LocalizationBoxes = new List<LocalizationBox>();
        private Dictionary<string, LocalizationBox> LocalizationBoxesDic = new Dictionary<string, LocalizationBox>();

        public void Init()
        {
            foreach (var localizationBox in LocalizationBoxes)
                LocalizationBoxesDic.Add(localizationBox.Key, localizationBox);
        }

        public LocalizationBox GetLocalizationBox(string key)
        {
            return LocalizationBoxesDic.GetValueOrDefault(key);
        }
    }
    
    [Serializable]
    public class LocalizationBox
    {
        public string Key;
        
        [TextArea(10, 10)] public string RusLocalization;
        [TextArea(10, 10)] public string EngLocalization;
        [TextArea(10, 10)] public string TurkLocalization;
    }
}
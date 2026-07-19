using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Localization")]
    public class LocalizationSo : ScriptableObject
    {
        public List<LocalizationBox> LocalizationBoxes = new ();
        public List<LocalizationBox> LocalizationBoxesGlobalShopItems = new ();
        public List<LocalizationBox> LocalizationBoxesTowers = new ();
        private Dictionary<string, LocalizationBox> LocalizationBoxesDic = new ();

        public void Init()
        {
            List<LocalizationBox> boxes = new List<LocalizationBox>();
            boxes.AddRange(LocalizationBoxes);
            boxes.AddRange(LocalizationBoxesGlobalShopItems);
            boxes.AddRange(LocalizationBoxesTowers);
            
            foreach (var localizationBox in boxes)
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
    }
}
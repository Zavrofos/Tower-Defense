using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameHubDir
{
    public class LevelButton : MonoBehaviour
    {
        [field: SerializeField] public int WorldNumber { get; private set; }
        [field: SerializeField] public int LevelNumber { get; private set; }
        [field: SerializeField] public Image LockImage { get; private set; }
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TMP_Text LevelNumberText { get; private set; }
        [field: SerializeField] public Color LockColor { get; private set; }
        [field: SerializeField] public Color UnLockColor { get; private set; }
        
        [field: SerializeField] public Color LockColorText { get; private set; }
        [field: SerializeField] public Color UnLockColorText { get; private set; }

        public void SetInteractable(bool value)
        {
            Button.interactable = value;
            LevelNumberText.color = value ? UnLockColorText : LockColorText;
            LockImage.gameObject.SetActive(!value);
        }
    }
}
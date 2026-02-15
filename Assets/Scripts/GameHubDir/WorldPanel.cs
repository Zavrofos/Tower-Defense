using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameHubDir
{
    public class WorldPanel : MonoBehaviour
    {
        [field: SerializeField] public Image LockImage { get; private set; }
        [field: SerializeField] public Image BackGroundImage { get; private set; }
        [field: SerializeField] public Image WorldIconImage { get; private set; }
        [field: SerializeField] public TMP_Text NameWorldText { get; private set; }
        [field: SerializeField] public Color LockColor { get; private set; }
        [field: SerializeField] public Color UnLockColor { get; private set; }
        [field: SerializeField] public List<LevelButton> LevelsButtons { get; private set; }

        public void SetInteractable(bool value)
        {
            LockImage.gameObject.SetActive(!value);
            BackGroundImage.color = value ? UnLockColor : LockColor;
            WorldIconImage.color = value ? UnLockColor : LockColor;
            NameWorldText.color = value ? UnLockColor : LockColor;
        }
    }
}
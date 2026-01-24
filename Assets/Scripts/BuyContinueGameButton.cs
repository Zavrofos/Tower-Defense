using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BuyContinueGameButton : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TMP_Text CountText { get; private set; }
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public Color EnableIconColor { get; private set; }
        [field: SerializeField] public Color DisableIconColor { get; private set; }
        [field: SerializeField] public Color EnableTextColor { get; private set; }
        [field: SerializeField] public Color DisableTextColor { get; private set; }

        public void SetInteractable(bool value)
        {
            Icon.color = value ? EnableIconColor : DisableIconColor;
            CountText.color = value ? EnableTextColor : DisableTextColor;
            Button.interactable = value;
        }
    }
}
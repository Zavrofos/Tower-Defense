using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonAbilityUI : MonoBehaviour
    {
        public Button Button;
        public Image Icon;
        public Image BackgroundCount;
        public TMP_Text Count;

        public Color EnableColor;
        public Color DisableColor;
        
        public void SetInteractableButton(bool value)
        {
            Button.interactable = value;
            Icon.color = value ? EnableColor : DisableColor;
            BackgroundCount.color = value ? EnableColor : DisableColor;
        }

        public void SetCount(int count)
        {
            Count.text = count.ToString();
        }
    }
}
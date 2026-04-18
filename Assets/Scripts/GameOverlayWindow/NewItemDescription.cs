using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class NewItemDescription : MonoBehaviour
    {
        [field: SerializeField] public LocalizationText Name { get; private set; }
        [field: SerializeField] public Image ImageDescription { get; private set; }
        [field: SerializeField] public LocalizationText DescriptionText { get; private set; }
    }
}
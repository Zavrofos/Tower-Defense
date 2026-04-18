using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GlobalShop
{
    public class DescriptionItem : MonoBehaviour
    {
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public LocalizationText Name { get; private set; }
        [field: SerializeField] public LocalizationText Description { get; private set; }
    }
}
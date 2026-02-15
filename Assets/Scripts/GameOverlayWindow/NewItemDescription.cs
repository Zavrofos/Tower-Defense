using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOverlayWindow
{
    public class NewItemDescription : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Name { get; private set; }
        [field: SerializeField] public Image ImageDescription { get; private set; }
        [field: SerializeField] public TMP_Text DescriptionText { get; private set; }
    }
}
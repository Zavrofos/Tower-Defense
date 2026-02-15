using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void OnValueChanged(float health, float maxHealth)
    {
        _slider.value = health / maxHealth;
    }

    private void OnEnable()
    {
        _slider.value = 1;
    }
}

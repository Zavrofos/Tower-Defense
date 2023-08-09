using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Home _home;
    [SerializeField] private Slider _slider;

    private void OnValueChanged(float health, float maxHealth)
    {
        _slider.value = health / maxHealth;
    }

    private void OnEnable()
    {
        _home.Wounded += OnValueChanged;
    }

    private void OnDisable()
    {
        _home.Wounded -= OnValueChanged;
    }

}

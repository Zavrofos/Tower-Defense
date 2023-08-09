using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform Transform;

    private void OnEnable()
    {
        Invoke(nameof(TurnOff), 0.2f);
    }
    private void TurnOff()
    {
        gameObject.SetActive(false);
    }
}

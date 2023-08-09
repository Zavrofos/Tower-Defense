using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private void OnMouseDown()
    {
        GameManagerInGame gameManager = FindObjectOfType<GameManagerInGame>();
        if (gameManager.IsDisableButtonColliders) return;
        gameManager.OpenShop(_shop);
        _shop.gameObject.SetActive(true);
    }
}

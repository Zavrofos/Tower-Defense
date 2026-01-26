using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private void OnMouseDown()
    {
        GameManagerInGame gameManager = GameManager.Instance.CurrentGameManagerLevel;
        if (gameManager.IsDisableButtonColliders) return;
        gameManager.OpenShop(_shop);
        _shop.gameObject.SetActive(true);
    }
}

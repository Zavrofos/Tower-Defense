using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.UIScripts;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private void OnMouseDown()
    {
        ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.CLickAudioUI);
        GameManagerInGame gameManager = GameManager.Instance.CurrentGameManagerLevel;
        if (gameManager.IsDisableButtonColliders) return;
        gameManager.OpenShop(_shop);
        _shop.gameObject.SetActive(true);
    }
}

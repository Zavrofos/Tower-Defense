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
        if(GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders)
            return;

        GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = true;
        ClickSoundPlayGlobal.Instance.Play(GameManager.Instance.GameAssets.CLickAudioUI);
        _shop.gameObject.SetActive(true);
    }
}

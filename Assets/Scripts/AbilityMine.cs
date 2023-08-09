using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMine : Ability
{
    public int Price = 10;
    private void Start()
    {
        GameManagerInGame GameManager = FindObjectOfType<GameManagerInGame>();
        GameManager.SpendCoins(Price);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy();
        }
    }
}

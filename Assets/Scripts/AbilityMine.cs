using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMine : Ability
{
    private ButtonAbility _buttonAbility;
    private bool _installed;

    private void Start()
    {
        _buttonAbility = GameManager.Instance.CurrentGameManagerLevel.MineButtonAbility;
    }

    private void Update()
    {
        if(_installed)
            return;
        
        float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        transform.position = new Vector2(x, y);

        if(Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            GameManager.Instance.CurrentGameData.CountMineBought++;
            _buttonAbility.CountText.text = GameManager.Instance.CurrentGameData.CountMineBought.ToString();
            _buttonAbility.AbilityButton.interactable = GameManager.Instance.CurrentGameData.CountMineBought > 0;
            SaveSystem.SaveSystem.SaveGame();
            Destroy(gameObject);
        }

        if(Input.GetMouseButtonDown(0))
        {
            transform.position = transform.position;
            GetComponent<Animator>().speed = 1;
            GetComponent<BoxCollider2D>().enabled = true;
            _installed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (!enemy.name.Contains("EnemyBoss"))
                enemy.ApplayDamage(enemy._health);
            else
                enemy.ApplayDamage(10);
            
            Destroy();
        }
    }
}

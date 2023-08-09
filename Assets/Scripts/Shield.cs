using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int Health;
    public float SpeedRotate;
    public SpriteRenderer SpriteRenderer;
    public Color ApplayDamageColor;
    private Color CurrentColor;
    public Coroutine ChangeColorForHitCoroutine;

    private void Start()
    {
        CurrentColor = SpriteRenderer.color;
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * SpeedRotate * Time.deltaTime);
    }

    public IEnumerator ChangeColorForHit()
    {
        SpriteRenderer.color = ApplayDamageColor;
        yield return new WaitForSeconds(0.2f);
        if(SpriteRenderer != null)
        {
            SpriteRenderer.color = CurrentColor;
        }
    }

    public void Destroy(EnemyShield enemy)
    {
        enemy._speed = enemy.SpeedAfterShilBroken;
        StopCoroutine(ChangeColorForHitCoroutine);
        Destroy(gameObject);
    }

    public void ChangeColorForHitStart()
    {
        ChangeColorForHitCoroutine = StartCoroutine(ChangeColorForHit());
    }
}

using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IApplayDamage
{
    public int Health;
    public float SpeedRotate;
    public SpriteRenderer SpriteRenderer;
    public Color ApplayDamageColor;
    private Color CurrentColor;
    public float SpeedAfterShilBroken;

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

    public void ApplayDamage(int damage)
    {
        Health -= damage;
        StartCoroutine(ChangeColorForHit());
        if (Health <= 0)
        {
            Enemy parent = GetComponentInParent<Enemy>();
            parent.MoveBehaviour.Speed = SpeedAfterShilBroken;
            Destroy(this.gameObject);
        }
    }
}

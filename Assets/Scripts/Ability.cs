using Assets.Scripts;
using System.Collections;
using UnityEngine;


public class Ability : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected Explosion _explosionPrefab;
    public float DamageRadius;
    [HideInInspector] public ButtonAbility ButtonAbility;

    protected void Destroy()
    {
        Explosion explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DamageRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                Enemy enemyObj = collider.gameObject.GetComponent<Enemy>();
                enemyObj.ApplayDamage(_damage);
            }
        }
        ButtonAbility.AudioExplosion.Play();
        Destroy(gameObject);
    }
}

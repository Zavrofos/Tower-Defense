using Assets.Scripts;
using Assets.Scripts.GlobalShop;
using Assets.Scripts.RepPoolObject;
using GameOverlayWindow;
using SaveSystemDir;
using UnityEngine;

public class MineAbility : MonoBehaviour
{
    [SerializeField] private AudioClip _setMineAudio;
    
    public AbilityMineButton AbilityMineButton { get; set; }
    private bool _installed;
    
    private void Update()
    {
        if (GameManager.Instance.GameOverMenu.gameObject.activeSelf ||
            GameManager.Instance.WinMenu.gameObject.activeSelf)
        {
            Destroy(gameObject);
            return;
        }
        
        if(_installed)
            return;
        
        float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        transform.position = new Vector2(x, y);

        if(Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].Count++;
            AbilityMineButton.CountText.text = SaveSystem.CurrentGameData.AbilityData[GlobalShopItemType.AbilityMine].Count.ToString();
            AbilityMineButton.Button.interactable = true;
            Destroy(gameObject);
        }

        if(Input.GetMouseButtonDown(0))
        {
            SoundBox soundBox = (SoundBox)GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.SoundBox, Vector3.zero, Quaternion.identity);
            soundBox.Play(_setMineAudio, false);
            
            transform.position = transform.position;
            GetComponent<Animator>().speed = 1;
            GetComponent<BoxCollider2D>().enabled = true;
            _installed = true;
        }
    }

    private void Destroy()
    {
        PooledObject pooledObj = GameManager.Instance.ObjectPooler.SpawnFromPool(PolledObjectType.ExplosionMine, transform.position, Quaternion.identity);
        Explosion explosion = (Explosion)pooledObj;
        explosion.ExplosonPlay();
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            IApplayDamage enemy = collision.gameObject.GetComponent<IApplayDamage>();

            if (!collision.name.Contains("EnemyBoss"))
                enemy.ApplayDamage(1000);
            else
                enemy.ApplayDamage(10);
            
            Destroy();
        }
    }
}

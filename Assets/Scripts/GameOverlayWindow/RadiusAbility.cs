using Assets.Scripts;
using UnityEngine;

namespace GameOverlayWindow
{
    public class RadiusAbility : MonoBehaviour
    {
        [field: SerializeField] public RocketAbility RocketAbility { get; set; }
        public AbilityRocketButton ButtonAbility { get; set; }
        
        private void Update()
        {
            if (GameManager.Instance.WinMenu.gameObject.activeSelf ||
                GameManager.Instance.GameOverMenu.gameObject.activeSelf)
            {
                Destroy(gameObject);
                return;
            }
            
            float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            transform.position = new Vector2(x, y);

            if(Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                ButtonAbility.ImageTime.fillAmount = 0;
                GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = false;
                Destroy(gameObject);
            }

            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(RocketAbility, transform.position, Quaternion.identity);
                ButtonAbility.IsReady = false;
                GameManager.Instance.CurrentGameManagerLevel.IsDisableButtonColliders = false;
                Destroy(gameObject);
            }
        }
    }
}
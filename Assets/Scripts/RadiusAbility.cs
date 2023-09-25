using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class RadiusAbility : MonoBehaviour
    {

        public Transform Transform;
        public Ability Ability;
        public GameManagerInGame gameManagerInGame;
        [HideInInspector] public ButtonAbility ButtonAbility;

        private void Start()
        {
            gameManagerInGame = FindObjectOfType<GameManagerInGame>();
        }

        private void Update()
        {
            float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            transform.position = new Vector2(x, y);

            if(Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                if (Ability is AbilityRocket) gameManagerInGame.RokketButtonAbility.ImageTime.fillAmount = 0;
                gameManagerInGame.IsDisableButtonColliders = false;
                Destroy(gameObject);
            }

            if(Input.GetMouseButtonDown(0))
            {
                Ability ability = Instantiate(Ability, transform.position, Quaternion.identity);
                ability.ButtonAbility = ButtonAbility;
                if(Ability is AbilityRocket)
                {
                    gameManagerInGame.RokketButtonAbility.IsReady = false;
                }
                gameManagerInGame.IsDisableButtonColliders = false;
                Destroy(gameObject);
            }
        }
    }
}
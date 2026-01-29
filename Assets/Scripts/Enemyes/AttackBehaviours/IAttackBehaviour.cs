namespace Assets.Scripts.Enemyes.AttackBehaviours
{
    public interface IAttackBehaviour
    {
        bool Attacking { get; set; }
        void Attack(IDamageSystem target);
    }
}
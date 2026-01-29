namespace Assets.Scripts.Enemyes.MoveBehaviours
{
    public interface IMoveBehaviour
    {
        float Speed { get; set; }
        void Move();
    }
}
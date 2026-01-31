namespace Assets.Scripts.Enemyes.MoveBehaviours
{
    public interface IMoveBehaviour
    {
        float Speed { get; set; }
        float CurrentSpeed { get; set; }
        void Move();
    }
}
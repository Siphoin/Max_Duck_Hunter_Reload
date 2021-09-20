namespace BaseEngine.Interfaces
{
    public interface IState<T>
    {
        void Enter();
        void Exit();
        void Update();
        void SetOwner(T owner);
    }
}

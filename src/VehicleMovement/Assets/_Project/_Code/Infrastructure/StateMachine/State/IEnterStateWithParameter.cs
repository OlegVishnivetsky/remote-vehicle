namespace _Project._Code.Infrastructure.StateMachine.State
{
    public interface IEnterStateWithParameter<in T> : IState
    {
        void Enter(T parameter);
    }
    
    public interface IEnterStateWithParameter<in T1, in T2> : IState
    {
        void Enter(T1 firstParameter, T2 secondParameter);
    }
}
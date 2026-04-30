namespace _Project._Code.Infrastructure.StateMachine.State
{
    public interface IStateWithCondition : IState
    {
        bool CanBeEntered();
    }
}
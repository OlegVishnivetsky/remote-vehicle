using _Project._Code.Infrastructure.StateMachine.State;

namespace _Project._Code.Infrastructure.StateMachine.StateFactory
{
    public interface IStateFactory 
    {
        IState Create<TState>() where TState : IState;
    }
}
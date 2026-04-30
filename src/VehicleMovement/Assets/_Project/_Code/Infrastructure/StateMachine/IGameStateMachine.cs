using _Project._Code.Infrastructure.StateMachine.State;

namespace _Project._Code.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        IState CurrentState { get; }
        
        void RegisterState<TState>(TState state) where TState : class, IState;
        void SwitchTo<TState>() where TState : class, IState;
        void SwitchTo<TState, TParameter>(TParameter parameter)
            where TState : class, IEnterStateWithParameter<TParameter>;
        void SwitchTo<TState, TParameter1, TParameter2>(TParameter1 parameter1, TParameter2 parameter2)
            where TState : class, IEnterStateWithParameter<TParameter1, TParameter2>;
    }
}
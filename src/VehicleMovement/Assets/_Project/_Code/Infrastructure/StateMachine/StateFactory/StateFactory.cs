using _Project._Code.Infrastructure.StateMachine.State;
using Zenject;

namespace _Project._Code.Infrastructure.StateMachine.StateFactory
{
    public class StateFactory : IStateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container) => _container = container;

        public IState Create<TState>() where TState : IState => _container.Resolve<TState>();
    }
}
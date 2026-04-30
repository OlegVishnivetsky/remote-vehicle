using _Project._Code.Infrastructure.StateMachine;
using _Project._Code.Infrastructure.States;
using Zenject;

namespace _Project._Code.Infrastructure
{
    public class Bootstrapper : IInitializable
    {
        private readonly IGameStateMachine _gameStateMachine;

        public Bootstrapper(IGameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

        public void Initialize() => _gameStateMachine.SwitchTo<BootState>();
    }
}
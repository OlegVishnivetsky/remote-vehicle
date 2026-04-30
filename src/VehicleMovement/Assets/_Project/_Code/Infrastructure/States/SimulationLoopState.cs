using _Project._Code.Infrastructure.StateMachine.State;
using _Project._Code.Services.Input;

namespace _Project._Code.Infrastructure.States
{
    public class SimulationLoopState : IEnterState
    {
        private readonly IInputService _inputService;
        
        public SimulationLoopState(IInputService inputService) => _inputService = inputService;
        
        public void Enter() => _inputService.Enable();
    }
}
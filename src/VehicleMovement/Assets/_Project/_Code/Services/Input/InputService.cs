using UnityEngine;
using Zenject;

namespace _Project._Code.Services.Input
{
    public class InputService : IInputService, IInitializable
    {
        private SimulationInput _simulationInput;
        
        public Vector2 Input => _simulationInput.Control.Move.ReadValue<Vector2>();
        public Vector2 Look => _simulationInput.Control.Look.ReadValue<Vector2>();
        
        public void Initialize()
        {
            _simulationInput = new();
            Enable();
        }
        
        public void Enable() => _simulationInput.Enable();

        public void Disable() => _simulationInput.Disable();
    }
}
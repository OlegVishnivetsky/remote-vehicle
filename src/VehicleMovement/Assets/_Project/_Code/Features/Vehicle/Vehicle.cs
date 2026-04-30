using System;
using System.Collections.Generic;
using _Project._Code.Features.Movement;
using _Project._Code.Services.Input;
using UnityEngine;
using Zenject;

namespace _Project._Code.Features.Vehicle
{
    public class Vehicle : MonoBehaviour
    {
        [SerializeField] private List<Transform> _wheels;

        private IInputService _inputService;
        private IVehicleMovementStrategy _movementStrategy;
        
        public List<Transform> Wheels => _wheels;
        
        [Inject]
        public void Construct(IInputService inputService) => _inputService = inputService;
        
        public void Initialize(IVehicleMovementStrategy movementStrategy) => _movementStrategy = movementStrategy;
        
        private void Update() => _movementStrategy.SimulateValues(_inputService.Input);

        private void FixedUpdate() => _movementStrategy.SimulatePhysics();

        private void OnDrawGizmos() => _movementStrategy?.DrawDebugGizmos();
    }
}
using _Project._Code.Features.Vehicle;
using UnityEngine;

namespace _Project._Code.Features.Movement
{
    public interface IVehicleMovementStrategy
    {
        void Initialize(VehicleContext context);
        void SimulateValues(Vector2 input);
        void SimulatePhysics();
        void DrawDebugGizmos();
    }
}
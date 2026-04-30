using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project._Code.Features.Vehicle.Factory
{
    public interface IVehicleFactory
    {
        UniTask<Vehicle> Create(VehicleTypeId typeId);
        void EnableCurrentVehicle(Vector3 atPosition);
        void DisableCurrentVehicle();
    }
}
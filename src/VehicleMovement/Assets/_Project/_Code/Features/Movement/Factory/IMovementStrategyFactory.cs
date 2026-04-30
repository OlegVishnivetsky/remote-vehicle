using _Project._Code.Features.Configs.VehicleMovement;
using _Project._Code.Features.Vehicle;

namespace _Project._Code.Features.Movement.Factory
{
    public interface IMovementStrategyFactory
    {
        IVehicleMovementStrategy CreateMovementStrategy(VehicleTypeId typeId, VehicleContext context, VehicleMovementConfig config);
    }
}
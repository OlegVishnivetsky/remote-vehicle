using _Project._Code.Features.Configs.VehicleMovement;
using _Project._Code.Features.Vehicle;

namespace _Project._Code.Features.Movement.Factory
{
    public class MovementStrategyFactory : IMovementStrategyFactory
    {
        public IVehicleMovementStrategy CreateMovementStrategy(VehicleTypeId typeId, VehicleContext context, VehicleMovementConfig config) =>
            typeId switch
            {
                VehicleTypeId.UGV => new RigidbodyUGVMovementStrategy(context.Rigidbody, config),
                _ => throw new System.NotImplementedException($"No strategy for {typeId}")
            };

    }
}
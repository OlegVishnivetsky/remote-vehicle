using _Project._Code.Features.Configs.Maps;
using _Project._Code.Features.Configs.VehicleMovement;
using _Project._Code.Features.Vehicle;
using Cysharp.Threading.Tasks;

namespace _Project._Code.Services.StaticData
{
    public interface IStaticDataService
    {
        UniTask LoadNecessaryDataAsync();
        VehicleMovementConfig GetMovementConfig(VehicleTypeId vehicleTypeId);
        MapsConfig GetMapsConfig();
    }
}
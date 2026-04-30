using System.Collections.Generic;
using _Project._Code.Features.Configs.Maps;
using _Project._Code.Features.Configs.VehicleMovement;
using _Project._Code.Features.Constants;
using _Project._Code.Features.Vehicle;
using _Project._Code.Services.AssetProvider;
using Cysharp.Threading.Tasks;
using ZLinq;

namespace _Project._Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService 
    {
        private readonly IAssetProviderService _assetProvider;
        
        private MapsConfig _mapsConfig;
        private IList<VehicleMovementConfig> _movementConfigs;

        public StaticDataService(IAssetProviderService assetProvider) => _assetProvider = assetProvider;

        public async UniTask LoadNecessaryDataAsync()
        {
            _mapsConfig = await _assetProvider.LoadAsset<MapsConfig>(RuntimeConstants.StaticDataAddresses.MapsConfig);
            
            _movementConfigs = await _assetProvider.LoadAssets<VehicleMovementConfig>(
                RuntimeConstants.StaticDataAddresses.VehicleMovements, null);
        }

        public VehicleMovementConfig GetMovementConfig(VehicleTypeId vehicleTypeId) => 
            _movementConfigs
                .AsValueEnumerable()
                .FirstOrDefault(x => x.VehicleType == vehicleTypeId);

        public MapsConfig GetMapsConfig() => _mapsConfig;
    }
}
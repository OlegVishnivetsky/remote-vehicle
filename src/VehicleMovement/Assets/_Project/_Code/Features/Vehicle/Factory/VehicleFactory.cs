using _Project._Code.Features.Configs.VehicleMovement;
using _Project._Code.Features.Constants;
using _Project._Code.Features.Movement;
using _Project._Code.Features.Movement.Factory;
using _Project._Code.Services.AssetProvider;
using _Project._Code.Services.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZLinq;

namespace _Project._Code.Features.Vehicle.Factory
{
    public class VehicleFactory : IVehicleFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IMovementStrategyFactory _movementStrategyFactory;

        private Vehicle _currentVehicle;
        
        public VehicleFactory(
            DiContainer container,
            IAssetProviderService assetProvider, 
            IStaticDataService staticDataService,
            IMovementStrategyFactory movementStrategyFactory)
        {
            _container = container;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _movementStrategyFactory = movementStrategyFactory;
        }

        public async UniTask<Vehicle> Create(VehicleTypeId typeId)
        {
            VehicleMovementConfig movementConfig = _staticDataService.GetMovementConfig(typeId);
            GameObject vehiclePrefab = await _assetProvider.LoadAsset<GameObject>(RuntimeConstants.PrefabAddresses.Vehicle);
            
            _currentVehicle = _container.InstantiatePrefabForComponent<Vehicle>(vehiclePrefab);
            _currentVehicle.gameObject.SetActive(false);

            VehicleContext context = new()
            {
                Wheels = _currentVehicle.Wheels,
                Root = _currentVehicle.transform,
                Rigidbody = _currentVehicle.GetComponent<Rigidbody>()
            };
            
            IVehicleMovementStrategy movementStrategy = _movementStrategyFactory.CreateMovementStrategy(typeId, context, movementConfig);
            movementStrategy.Initialize(context);
            _currentVehicle.Initialize(movementStrategy);
            
            return _currentVehicle;
        }
        
        public void EnableCurrentVehicle(Vector3 atPosition)
        {
            Rigidbody rb = _currentVehicle.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            _currentVehicle.transform.SetPositionAndRotation(atPosition, Quaternion.identity);
            _currentVehicle.gameObject.SetActive(true);
        }
        
        public void DisableCurrentVehicle()
        {
            Rigidbody rb = _currentVehicle.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            _currentVehicle.gameObject.SetActive(false);
        }
    }
}
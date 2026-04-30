using System;
using _Project._Code.Features.Configs.Maps;
using _Project._Code.Features.FollowCamera;
using _Project._Code.Features.FollowCamera.Factory;
using _Project._Code.Features.HudScreen;
using _Project._Code.Features.MapLoader;
using _Project._Code.Features.Vehicle;
using _Project._Code.Features.Vehicle.Factory;
using _Project._Code.Infrastructure.StateMachine;
using _Project._Code.Infrastructure.StateMachine.State;
using _Project._Code.Services.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project._Code.Infrastructure.States
{
    public class InitializeSimulationState : IEnterState
    {
        private readonly HudScreen _hudScreen;
        private readonly IVehicleFactory _vehicleFactory;
        private readonly IFollowCameraFactory _followCameraFactory;
        private readonly IGameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;

        public InitializeSimulationState(
            HudScreen hudScreen,
            IVehicleFactory vehicleFactory,
            IFollowCameraFactory followCameraFactory,
            IGameStateMachine stateMachine,
            IStaticDataService staticDataService)
        {
            _hudScreen = hudScreen;
            _vehicleFactory = vehicleFactory;
            _followCameraFactory = followCameraFactory;
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }

        public void Enter() => InitializeAsync().Forget();

        private async UniTaskVoid InitializeAsync()
        {
            try
            {
                _hudScreen.gameObject.SetActive(IsMobile());
                
                Vehicle vehicle = await _vehicleFactory.Create(VehicleTypeId.UGV);
                
                IFollowCamera followCamera = await _followCameraFactory.CreateCinemachineFollowCamera();
                followCamera.SetTarget(vehicle.transform);
                
                MapsConfig mapsConfig = _staticDataService.GetMapsConfig();
                _stateMachine.SwitchTo<LoadMapState, MapTypeId>(mapsConfig.DefaultMap);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error while initializing simulation: {e}");
            }
        }
        
        private bool IsMobile()
        {
#if UNITY_EDITOR
            return UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android
                   || UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS;
#else
            return Application.isMobilePlatform;
#endif
        }
    }
}
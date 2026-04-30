using System;
using _Project._Code.Features.LoadingScreen;
using _Project._Code.Features.MapLoader;
using _Project._Code.Features.Vehicle.Factory;
using _Project._Code.Infrastructure.StateMachine;
using _Project._Code.Infrastructure.StateMachine.State;
using _Project._Code.Services.Input;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project._Code.Infrastructure.States
{
    public class LoadMapState : IEnterStateWithParameter<MapTypeId>
    {
        private readonly LoadingScreen _loadingScreen;
        private readonly IMapLoader _mapLoader;
        private readonly IInputService _inputService;
        private readonly IVehicleFactory _vehicleFactory;
        private readonly IGameStateMachine _stateMachine;

        public LoadMapState(
            LoadingScreen loadingScreen,
            IMapLoader mapLoader,
            IInputService inputService,
            IVehicleFactory vehicleFactory,
            IGameStateMachine stateMachine)
        {
            _loadingScreen = loadingScreen;
            _mapLoader = mapLoader;
            _inputService = inputService;
            _vehicleFactory = vehicleFactory;
            _stateMachine = stateMachine;
        }

        public void Enter(MapTypeId typeId) => LoadMapAsync(typeId).Forget();

        private async UniTask LoadMapAsync(MapTypeId typeId)
        {
            try
            {
                _loadingScreen.Show();
                _inputService.Disable();
                _mapLoader.UnloadMap();
                
                Map map = await _mapLoader.LoadMap(typeId);
                await UniTask.WaitForFixedUpdate();
                
                _vehicleFactory.EnableCurrentVehicle(map.SpawnPoint.position);
                _stateMachine.SwitchTo<SimulationLoopState>();
                _loadingScreen.Hide();
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error while loading map: {e}");
            }
        }
    }
}
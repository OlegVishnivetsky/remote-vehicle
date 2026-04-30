using _Project._Code.Features.FollowCamera.Factory;
using _Project._Code.Features.HudScreen;
using _Project._Code.Features.MapLoader;
using _Project._Code.Features.MapLoader.View;
using _Project._Code.Features.Movement.Factory;
using _Project._Code.Features.Vehicle.Factory;
using _Project._Code.Infrastructure.StateMachine;
using _Project._Code.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace _Project._Code.Infrastructure.DI
{
    public class SimulationInstaller : MonoInstaller
    {
        [SerializeField] private MapLoaderView _mapLoaderView;
        [SerializeField] private HudScreen _hudScreen;
        
        public override void InstallBindings()
        {
            BindHudScreen();
            BindMapLoader();
            BindFactories();
            BindSimulationStates();
        }

        private void BindHudScreen() => Container.Bind<HudScreen>().FromComponentInNewPrefab(_hudScreen).AsSingle();

        private void BindMapLoader()
        {
            Container.Bind<IMapLoader>().To<MapLoader>().AsSingle();
            Container.Bind<MapLoaderView>().FromComponentInNewPrefab(_mapLoaderView).AsSingle().NonLazy();
        }

        private void BindFactories()
        {
            Container.Bind<IFollowCameraFactory>().To<FollowCameraFactory>().AsSingle();
            Container.Bind<IMovementStrategyFactory>().To<MovementStrategyFactory>().AsSingle();
            Container.Bind<IVehicleFactory>().To<VehicleFactory>().AsSingle();
        }

        private void BindSimulationStates()
        {
            Container.Bind<InitializeSimulationState>().AsSingle();
            Container.Bind<LoadMapState>().AsSingle();
            Container.Bind<SimulationLoopState>().AsSingle();
        }

        public override void Start()
        {
            base.Start();
            
            IGameStateMachine stateMachine = Container.Resolve<IGameStateMachine>();
            InitializeSimulationState initializeSimulationState = Container.Resolve<InitializeSimulationState>();
            LoadMapState loadMapState = Container.Resolve<LoadMapState>();
            SimulationLoopState simulationLoopState = Container.Resolve<SimulationLoopState>();
            
            stateMachine.RegisterState(initializeSimulationState);
            stateMachine.RegisterState(loadMapState);
            stateMachine.RegisterState(simulationLoopState);
            stateMachine.SwitchTo<InitializeSimulationState>();
        }
    }
}
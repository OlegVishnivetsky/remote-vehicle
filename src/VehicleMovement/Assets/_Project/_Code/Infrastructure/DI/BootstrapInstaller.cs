using _Project._Code.Features.LoadingScreen;
using _Project._Code.Infrastructure.StateMachine;
using _Project._Code.Infrastructure.StateMachine.StateFactory;
using _Project._Code.Infrastructure.States;
using _Project._Code.Services.AssetProvider;
using _Project._Code.Services.Input;
using _Project._Code.Services.SceneLoader;
using _Project._Code.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _Project._Code.Infrastructure.DI
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        
        public override void InstallBindings()
        {
            BindSceneLoaderService();
            BindAssetProviderService();
            BindStaticDataService();
            BindFactories();
            BindInputService();
            CreateAndBindLoadingScreen();
            BindStateMachine();
            BindBootstrapper();
        }

        private void BindSceneLoaderService() => Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
        
        private void BindAssetProviderService() => Container.Bind<IAssetProviderService>().To<AssetProviderService>().AsSingle();
        
        private void BindStaticDataService() => Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        
        private void BindFactories() => Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();

        private void BindInputService() => Container.BindInterfacesTo<InputService>().AsSingle();

        private void CreateAndBindLoadingScreen() => Container.Bind<LoadingScreen>().FromComponentInNewPrefab(_loadingScreen).AsSingle();

        private void BindStateMachine()
        {
            Container.Bind<BootState>().AsSingle();
            Container.BindInterfacesTo<GameStateMachine>().AsSingle();
        }

        private void BindBootstrapper() => Container.BindInterfacesTo<Bootstrapper>().AsSingle();
    }
}
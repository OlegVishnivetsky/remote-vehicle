using System;
using _Project._Code.Features.Constants;
using _Project._Code.Features.LoadingScreen;
using _Project._Code.Infrastructure.StateMachine.State;
using _Project._Code.Services.SceneLoader;
using _Project._Code.Services.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project._Code.Infrastructure.States
{
    public class BootState : IEnterState
    {
        private readonly LoadingScreen _loadingScreen;
        private readonly ISceneLoaderService _sceneLoader;
        private readonly IStaticDataService _staticDataService;

        public BootState(
            LoadingScreen loadingScreen,
            ISceneLoaderService sceneLoader,
            IStaticDataService staticDataService)
        {
            _loadingScreen = loadingScreen;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
        }

        public void Enter() => BootAsync().Forget();

        private async UniTask BootAsync()
        {
            try
            {
                _loadingScreen.Show();
                
                await _staticDataService.LoadNecessaryDataAsync();
                
                _sceneLoader.Load(RuntimeConstants.SceneNames.Simulation);
                
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error while booting: {e}");
            }
        }
    }
}
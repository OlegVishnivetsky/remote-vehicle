using _Project._Code.Features.Constants;
using _Project._Code.Services.AssetProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project._Code.Features.FollowCamera.Factory
{
    public class FollowCameraFactory : IFollowCameraFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetProviderService _assetProvider;

        public FollowCameraFactory(DiContainer container, IAssetProviderService assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public async UniTask<IFollowCamera> CreateCinemachineFollowCamera()
        {
            GameObject cameraPrefab = await _assetProvider.LoadAsset<GameObject>(RuntimeConstants.PrefabAddresses.FollowCamera);
            IFollowCamera followCamera = _container.InstantiatePrefabForComponent<IFollowCamera>(cameraPrefab);
            
            return followCamera;
        }
    }
}
using _Project._Code.Features.Configs.Maps;
using _Project._Code.Services.AssetProvider;
using _Project._Code.Services.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project._Code.Features.MapLoader
{
    public class MapLoader : IMapLoader
    {
        private readonly IAssetProviderService _assetProvider;
        private readonly IStaticDataService _staticDataService;
        
        private Map _currentMapInstance;
        private AssetReferenceGameObject _currentMapReference;

        public MapLoader(IAssetProviderService assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public async UniTask<Map> LoadMap(MapTypeId typeId)
        {
            MapsConfig config = _staticDataService.GetMapsConfig();
            MapData data = config.GetMapData(typeId);
            GameObject mapPrefab = await _assetProvider.LoadAsset<GameObject>(data.Prefab);

            _currentMapReference = data.Prefab;
            _currentMapInstance = Object.Instantiate(mapPrefab).GetComponent<Map>();
            
            return _currentMapInstance;
        }

        public void UnloadMap()
        {
            if (_currentMapInstance == null)
                return;
            
            Object.Destroy(_currentMapInstance.gameObject);
            _assetProvider.UnloadAssets(_currentMapReference.AssetGUID);
            _currentMapInstance = null;
            _currentMapReference = null;
        }
    }
}
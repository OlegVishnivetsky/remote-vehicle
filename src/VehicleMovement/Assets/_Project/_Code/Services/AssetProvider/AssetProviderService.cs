using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project._Code.Services.AssetProvider
{
    public class AssetProviderService : IAssetProviderService 
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetsCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _allHandles = new();
        
        public async UniTask InitializeAsync() => await Addressables.InitializeAsync();
        
        public async UniTask<IList<T>> LoadAssets<T>(string assetsFolderAddress, Action<T> everyAssetCallback)
            where T : class
        {
            AsyncOperationHandle<IList<T>> handle =
                Addressables.LoadAssetsAsync(assetsFolderAddress, everyAssetCallback);
            
            if (!_allHandles.ContainsKey(assetsFolderAddress))
                _allHandles[assetsFolderAddress] = new List<AsyncOperationHandle>();

            _allHandles[assetsFolderAddress].Add(handle);

            return await handle.Task;
        }
        
        public async UniTask<T> LoadAsset<T>(string assetAddress) where T : class
        {
            if (_assetsCache.TryGetValue(assetAddress, out AsyncOperationHandle assetHandle))
                return assetHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetAddress);

            if (!_allHandles.ContainsKey(assetAddress))
                _allHandles[assetAddress] = new List<AsyncOperationHandle>();

            _allHandles[assetAddress].Add(handle);

            handle.Completed += (asyncOperationHandle) =>
            {
                _assetsCache[assetAddress] = asyncOperationHandle;
            };

            return await handle.Task;
        }
        
        public async UniTask<T> LoadAsset<T>(AssetReferenceGameObject gameObjectReference) where T : class
        {
            if (_assetsCache.TryGetValue(gameObjectReference.AssetGUID, out AsyncOperationHandle assetHandle))
                return assetHandle.Result as T;

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(gameObjectReference);

            if (!_allHandles.ContainsKey(gameObjectReference.AssetGUID))
                _allHandles[gameObjectReference.AssetGUID] = new List<AsyncOperationHandle>();

            _allHandles[gameObjectReference.AssetGUID].Add(handle);

            handle.Completed += (asyncOperationHandle) =>
            {
                _assetsCache[gameObjectReference.AssetGUID] = asyncOperationHandle;
            };

            return await handle.Task;
        }

        public void UnloadAssets(string assetsFolderAddress)
        {
            if (!_allHandles.TryGetValue(assetsFolderAddress, out List<AsyncOperationHandle> assetHandles))
                return;

            foreach (AsyncOperationHandle handle in assetHandles)
                handle.Release();

            _allHandles.Remove(assetsFolderAddress);
            _assetsCache.Remove(assetsFolderAddress);
        }
    }
}
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace _Project._Code.Services.AssetProvider
{
    public interface IAssetProviderService
    {
        UniTask InitializeAsync();
        UniTask<IList<T>> LoadAssets<T>(string assetsFolderAddress, Action<T> everyAssetCallback) where T : class;
        UniTask<T> LoadAsset<T>(string assetAddress) where T : class;
        UniTask<T> LoadAsset<T>(AssetReferenceGameObject gameObjectReference) where T : class;
        void UnloadAssets(string assetsFolderAddress);
    }
}
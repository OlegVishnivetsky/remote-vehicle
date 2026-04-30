using System;
using _Project._Code.Features.MapLoader;
using UnityEngine.AddressableAssets;

namespace _Project._Code.Features.Configs.Maps
{
    [Serializable]
    public class MapData
    {
        public MapTypeId TypeId;
        public AssetReferenceGameObject Prefab;
    }
}
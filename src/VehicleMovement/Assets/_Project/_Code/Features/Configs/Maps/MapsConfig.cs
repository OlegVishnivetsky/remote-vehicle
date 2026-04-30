using System.Collections.Generic;
using _Project._Code.Features.MapLoader;
using UnityEngine;
using ZLinq;

namespace _Project._Code.Features.Configs.Maps
{
    [CreateAssetMenu(fileName = "Maps Config", menuName = "Configs/Maps Config")]
    public class MapsConfig : ScriptableObject
    {
        public MapTypeId DefaultMap;
        public List<MapData> Maps;
        
        public MapData GetMapData(MapTypeId typeId) => 
            Maps
                .AsValueEnumerable()
                .FirstOrDefault(x => x.TypeId == typeId);
    }
}
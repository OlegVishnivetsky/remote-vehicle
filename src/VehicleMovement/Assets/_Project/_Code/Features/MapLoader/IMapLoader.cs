using Cysharp.Threading.Tasks;

namespace _Project._Code.Features.MapLoader
{
    public interface IMapLoader
    {
        UniTask<Map> LoadMap(MapTypeId typeId);
        void UnloadMap();
    }
}
using Cysharp.Threading.Tasks;

namespace _Project._Code.Features.FollowCamera.Factory
{
    public interface IFollowCameraFactory
    {
        UniTask<IFollowCamera> CreateCinemachineFollowCamera();
    }
}
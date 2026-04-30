using System;

namespace _Project._Code.Services.SceneLoader
{
    public interface ISceneLoaderService
    {
        void Load(string sceneName, Action callback = null);
    }
}
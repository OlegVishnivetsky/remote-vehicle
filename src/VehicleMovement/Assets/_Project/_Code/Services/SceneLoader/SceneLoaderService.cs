using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project._Code.Services.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public void Load(string sceneName, Action callback = null) => LoadScene(sceneName, callback).Forget();

        private async UniTaskVoid LoadScene(string sceneName, Action callback)
        {
            AsyncOperation loadNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (loadNextScene is { isDone: false })
                await UniTask.Yield();
            
            callback?.Invoke();
        }
    }
}
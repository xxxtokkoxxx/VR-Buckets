using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _VRBuckets.CodeBase.Services
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public async UniTask LoadScene(string sceneName)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
            await loadingOperation;
        }
    }
}
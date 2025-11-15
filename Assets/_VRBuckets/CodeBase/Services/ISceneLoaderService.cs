using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Services
{
    public interface ISceneLoaderService
    {
        UniTask LoadScene(string sceneName);
    }
}
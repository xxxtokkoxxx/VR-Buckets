using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Services
{
    public interface ISceneLoadingService
    {
        UniTask LoadScene(string sceneName);
    }
}
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Configuration
{
    public interface IGameplayConfiguration
    {
        float MatchTIme { get; }
        int ScoresToWIn { get; }

        UniTask LoadAndSetConfiguration();
        void ReleaseConfigurationAsset();
    }
}
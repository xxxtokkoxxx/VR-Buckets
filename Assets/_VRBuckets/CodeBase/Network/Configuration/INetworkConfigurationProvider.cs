using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Network.Configuration
{
    public interface INetworkConfigurationProvider
    {
        int MaxPlayersCountPerRoom { get; }
        UniTask LoadAndSetConfiguration();
        void ReleaseConfigurationAsset();
    }
}
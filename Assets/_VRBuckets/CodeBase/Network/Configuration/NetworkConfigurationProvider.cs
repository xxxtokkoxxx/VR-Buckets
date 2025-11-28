using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Network.Configuration
{
    public class NetworkConfigurationProvider : INetworkConfigurationProvider
    {
        private readonly IAssetLoaderService _assetLoaderService;

        public NetworkConfigurationProvider(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public int MaxPlayersCountPerRoom { get; private set; }

        public async UniTask LoadAndSetConfiguration()
        {
            NetworkConfigurationSO asset = await _assetLoaderService.LoadAsset<NetworkConfigurationSO>(AssetsDataPath.NetworkConfiguration);
            MaxPlayersCountPerRoom = asset.MaxPlayersCountPerRoom;
        }

        public void ReleaseConfigurationAsset()
        {
            _assetLoaderService.Release(AssetsDataPath.NetworkConfiguration);
        }
    }
}
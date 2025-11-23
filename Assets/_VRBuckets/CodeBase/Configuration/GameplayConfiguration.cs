using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Configuration
{
    public class GameplayConfiguration : IGameplayConfiguration
    {
        private readonly IAssetLoaderService _assetLoaderService;
        public GameplayConfiguration(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public float MatchTIme { get; private set; }
        public int ScoresToWIn { get; private set; }

        public async UniTask LoadAndSetConfiguration()
        {
            GameConfigSO asset = await _assetLoaderService.LoadAsset<GameConfigSO>(AssetsDataPath.GameConfig);
            MatchTIme = asset.MatchTime;
            ScoresToWIn = asset.ScoresToWin;
        }

        public void ReleaseConfigurationAsset()
        {
            _assetLoaderService.Release(AssetsDataPath.GameConfig);
        }
    }
}
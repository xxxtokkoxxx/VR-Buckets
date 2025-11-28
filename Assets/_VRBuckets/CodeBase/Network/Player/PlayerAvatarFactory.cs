using System.Collections.Generic;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Network.Player
{
    public class PlayerAvatarFactory : IPlayerAvatarFactory
    {
        private PlayerAvatar _avatarReference;
        private List<PlayerAvatar> _createdAvatars = new();
        private readonly IAssetLoaderService _assetLoaderService;

        public PlayerAvatarFactory(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public async UniTask LoadPlayerAvatar()
        {
            _avatarReference = await _assetLoaderService.LoadPrefab<PlayerAvatar>(AssetsDataPath.PlayerAvatar);
        }
    }

    public interface IPlayerAvatarFactory
    {

    }
}
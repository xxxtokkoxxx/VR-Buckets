using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.Factory;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public class PlayerFactory : BaseFactory, IPlayerFactory
    {
        private PlayerAvatar _avatarReference;
        private List<PlayerAvatar> _createdAvatars = new();
        private readonly IAssetLoaderService _assetLoaderService;

        public PlayerFactory(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public async UniTask LoadPlayerAvatar()
        {
            _avatarReference = await _assetLoaderService.LoadPrefab<PlayerAvatar>(AssetsDataPath.PlayerAvatar);
        }

        public PlayerAvatar CreateAvatar(Guid id, Transform position)
        {
            PlayerAvatar avatar = Create(_avatarReference);
            _createdAvatars.Add(avatar);
            return avatar;
        }
    }
}
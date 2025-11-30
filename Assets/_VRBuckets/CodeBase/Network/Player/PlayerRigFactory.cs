using System.Collections.Generic;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.Factory;
using _VRBuckets.CodeBase.Network.Connection;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using Fusion.XR.Shared.Core;
using UnityEngine;

namespace _VRBuckets.CodeBase.Network.Player
{
    public class PlayerRigFactory : BaseFactory, IPlayerRigFactory
    {
        private NetworkRig _networkRigReference;
        private List<NetworkRig> _createdRigs = new();

        private readonly IAssetLoaderService _assetLoaderService;
        private readonly INetworkConnectionRunner _networkConnectionRunner;

        public PlayerRigFactory(IAssetLoaderService assetLoaderService,
            INetworkConnectionRunner networkConnectionRunner)
        {
            _assetLoaderService = assetLoaderService;
            _networkConnectionRunner = networkConnectionRunner;
        }

        public async UniTask LoadNetworkRig()
        {
            _networkRigReference = await _assetLoaderService.LoadPrefab<NetworkRig>(AssetsDataPath.PlayerNetworkRig);
        }

        public NetworkRig CreateNetworkRig(Vector3 position)
        {
            NetworkRig rig = CreateNetworkObject(_networkConnectionRunner.NetworkRunner, _networkRigReference, position,
                Quaternion.identity);

            _createdRigs.Add(rig);
            return rig;
        }

        public void Release()
        {
            _createdRigs.Clear();
            _assetLoaderService.Release(_networkRigReference);
        }
    }
}
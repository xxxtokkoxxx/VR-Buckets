using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.Factory;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _VRBuckets.CodeBase.GamePlay.Bucket
{
    public class HoopFactory : BaseFactory, IHoopFactory
    {
        private HoopView _hoopReference;
        private Dictionary<int, HoopView> _cratedHoops = new();

        private readonly IAssetLoaderService _assetLoaderService;
        private readonly IObjectResolver _resolver;

        public HoopFactory(IAssetLoaderService assetLoaderService, IObjectResolver resolver)
        {
            _assetLoaderService = assetLoaderService;
            _resolver = resolver;
        }

        public async UniTask LoadHoopReference()
        {
            _hoopReference = await _assetLoaderService.LoadPrefab<HoopView>(AssetsDataPath.Hoop);
        }

        public HoopView CreateHoop(Transform parent, int playerId)
        {
            bool hoopExists = _cratedHoops.TryGetValue(playerId, out HoopView cachedHoop);

            if (!hoopExists)
            {
                return CreateHoopInternal(parent, playerId);
            }

            cachedHoop.transform.parent = parent;
            cachedHoop.transform.localPosition = Vector3.zero;
            return cachedHoop;
        }

        public void Release()
        {
            _assetLoaderService.Release(_hoopReference);
        }

        private HoopView CreateHoopInternal(Transform position, int playerId)
        {
            HoopView hoop = CreateWithDependencyInjection(_resolver, _hoopReference, position);
            hoop.Initialize(playerId);
            _cratedHoops.Add(playerId, hoop);
            return hoop;
        }
    }
}
using System.Collections.Generic;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.Factory;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Bucket
{
    public class HoopFactory : BaseFactory, IHoopFactory
    {
        private HoopView _hoopReference;
        private List<HoopView> _bucketsPool = new();
        private int _poolSize = 5;
        private int _poolIndex;

        private readonly IAssetLoaderService _assetLoaderService;

        public HoopFactory(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public async UniTask LoadHoopReference()
        {
            _hoopReference = await _assetLoaderService.LoadAsset<HoopView>(AssetsDataPath.Hoop);
        }

        public HoopView CreateBucket(Transform position)
        {
            if (_bucketsPool.Count < _poolSize)
            {
                HoopView ball = Create(_hoopReference, position);
                _bucketsPool.Add(ball);
            }
            else
            {
                if (_poolIndex >= _poolSize)
                {
                    _poolIndex = 0;
                }

                return _bucketsPool[_poolIndex++];
            }

            return Create(_hoopReference, position);
        }

        public void SetPoolSize(int poolSize)
        {
            _poolSize = poolSize;
        }
    }
}
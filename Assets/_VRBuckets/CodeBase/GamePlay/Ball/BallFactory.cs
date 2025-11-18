using System.Collections.Generic;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.Factory;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Ball
{
    public class BallFactory : BaseFactory, IBallFactory
    {
        private readonly IAssetLoaderService _assetLoaderService;
        private BallView _ballReference;
        private List<BallView> _ballPool = new();
        private int _ballPoolSize = 5;
        private int _poolIndex;

        public BallFactory(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public async UniTask LoadBallReference()
        {
            _ballReference = await _assetLoaderService.LoadAsset<BallView>(AssetsDataPath.Ball);
        }

        public void SetPoolSize(int poolSize)
        {
            _ballPoolSize = poolSize;
        }

        public BallView CreateBall(Transform position)
        {
            if (_ballPool.Count < _ballPoolSize)
            {
                BallView ball = Create(_ballReference, position);
                _ballPool.Add(ball);
            }
            else
            {
                if (_poolIndex >= _ballPoolSize)
                {
                    _poolIndex = 0;
                }

                return _ballPool[_poolIndex++];
            }

            return Create(_ballReference, position);
        }
    }
}
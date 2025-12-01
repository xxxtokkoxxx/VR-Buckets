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
        private List<BallView> _createdBalls = new();

        public BallFactory(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public async UniTask LoadBallReference()
        {
            _ballReference = await _assetLoaderService.LoadPrefab<BallView>(AssetsDataPath.Ball);
        }

        public BallView CreateBall(Transform position, int playerId)
        {
            Debug.Log("call CreateBall, ball reference " + _ballReference);
            BallView ball = CreateNetworkObject(_ballReference, position.position, Quaternion.identity);

            ball.Initialize(playerId);
            _createdBalls.Add(ball);

            Debug.Log("should create it");
            return ball;
        }

        public List<BallView> GetCreatedBalls() => _createdBalls;

        public void Release()
        {
            _assetLoaderService.Release(_ballReference);
        }
    }
}
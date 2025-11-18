using System.Collections.Generic;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.Factory;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Environment
{
    public class EnvironmentFactory : BaseFactory, IEnvironmentFactory
    {
        private BasketballCourtView _courtViewReference;
        private List<BasketballCourtView> _createdCourts = new();

        private readonly IAssetLoaderService _assetLoaderService;

        public EnvironmentFactory(IAssetLoaderService assetLoaderService)
        {
            _assetLoaderService = assetLoaderService;
        }

        public async UniTask LoadEnvironment()
        {
            _courtViewReference =
                await _assetLoaderService.LoadAsset<BasketballCourtView>(AssetsDataPath.Court);
        }

        public BasketballCourtView CrateBasketballCourt(Transform position, Transform parent)
        {
            BasketballCourtView court = Create(_courtViewReference, position);
            return court;
        }

        public void DestroyCourts()
        {
            foreach (BasketballCourtView court in _createdCourts)
            {
                Destroy(court.gameObject);
            }

            _createdCourts.Clear();

            _assetLoaderService.Release(_courtViewReference);
        }
    }
}
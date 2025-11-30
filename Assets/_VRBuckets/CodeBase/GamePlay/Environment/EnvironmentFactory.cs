using System;
using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Infrastructure.DI;
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
        private readonly IMonoBehaviourProvider _monoBehaviourProvider;

        public EnvironmentFactory(IAssetLoaderService assetLoaderService, IMonoBehaviourProvider monoBehaviourProvider)
        {
            _assetLoaderService = assetLoaderService;
            _monoBehaviourProvider = monoBehaviourProvider;
        }

        public async UniTask LoadEnvironment()
        {
            _courtViewReference =
                await _assetLoaderService.LoadPrefab<BasketballCourtView>(AssetsDataPath.Court);
        }

        public BasketballCourtView CrateBasketballCourt(Transform position, Transform parent, int playerId)
        {
            Vector3 userPos = _monoBehaviourProvider.UserCameraTransform.transform.position;
            Vector3 groundPos = _monoBehaviourProvider.GroundTransform.transform.position;

            BasketballCourtView court = Create(_courtViewReference, position.position, Quaternion.identity, null);
            court.Initialize(playerId);

            Vector3 courtPos = userPos - court.transform.TransformPoint(court.PlayerInitPoint.localPosition);
            courtPos.y = groundPos.y;
            court.transform.position = courtPos;
            _createdCourts.Add(court);
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

        public void Release()
        {
            _assetLoaderService.Release(_courtViewReference);
        }

        public BasketballCourtView GetCourt(int playerId)
        {
            BasketballCourtView court = _createdCourts.FirstOrDefault(a=>a.PlayerId == playerId);

            if (court == null)
            {
                throw new NullReferenceException("Couldn't find court for player " + playerId);
            }

            return court;
        }
    }
}
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Environment
{
    public interface IEnvironmentFactory
    {
        UniTask LoadEnvironment();
        BasketballCourtView CrateBasketballCourt(Transform position, Transform parent);
        BasketballCourtView GetCourt(Guid playerId);
        void DestroyCourts();
    }
}
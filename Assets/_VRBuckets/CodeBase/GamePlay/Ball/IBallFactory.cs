using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Ball
{
    public interface IBallFactory
    {
        UniTask LoadBallReference();
        BallView CreateBall(Transform position, Guid playerId);
        List<BallView> GetCreatedBalls();
        void Release();
    }
}
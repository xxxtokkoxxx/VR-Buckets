using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Ball
{
    public interface IBallFactory
    {
        UniTask LoadBallReference();
        BallView CreateBall(Transform position);
        void SetPoolSize(int poolSize);
        List<BallView> GetCreatedBalls();
    }
}
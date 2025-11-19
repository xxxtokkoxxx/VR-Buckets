using System;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public interface IGameplayProcessor
    {
        void EnrollScore(Guid playerId, int score);
    }
}
using System;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public interface IGameplayProcessor
    {
        event Action<Guid> OnGameFinished;
        void EnrollScore(Guid playerId, int score);
    }
}
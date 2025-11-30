using System;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public interface IGameplayProcessor
    {
        event Action OnGameFinished;
        void EnrollScore(int playerId, int score);
    }
}
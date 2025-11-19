using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.GamePlay.Player;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public class GameplayProcessor : IGameplayProcessor
    {
        private Dictionary<Guid, PlayerEntity> _playerEntities = new();

        public void EnrollScore(Guid playerId, int score)
        {
            bool entityExists = _playerEntities.TryGetValue(playerId, out PlayerEntity playerEntity);

            if (!entityExists)
            {
                throw new KeyNotFoundException($"Player {playerId} does not exist");
            }
            
            playerEntity.SetScore(playerEntity.Score + score);
        }

        private void OnBallHitsHoop()
        {
            
        }

        private void OnEnrollingScore()
        {
            
        }

        private void SpawnHoop()
        {
            
        }

        private void SpawnBall()
        {
            
        }
    }
}
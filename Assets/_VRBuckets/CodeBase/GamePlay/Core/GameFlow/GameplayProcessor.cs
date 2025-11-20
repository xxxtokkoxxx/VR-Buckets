using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.Configuration;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.GamePlay.Player;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public class GameplayProcessor : IGameplayProcessor
    {
        private Dictionary<Guid, PlayerEntity> _playerEntities = new();

        private readonly IGameplayConfiguration _gameplayConfiguration;
        private readonly IHoopFactory _hoopFactory;
        private readonly IEnvironmentFactory _environmentFactory;

        public GameplayProcessor(IGameplayConfiguration gameplayConfiguration, IHoopFactory hoopFactory,
            IEnvironmentFactory environmentFactory)
        {
            _gameplayConfiguration = gameplayConfiguration;
            _hoopFactory = hoopFactory;
            _environmentFactory = environmentFactory;
        }

        public void EnrollScore(Guid playerId, int score)
        {
            bool entityExists = _playerEntities.TryGetValue(playerId, out PlayerEntity playerEntity);

            if (!entityExists)
            {
                throw new KeyNotFoundException($"Player {playerId} does not exist");
            }

            playerEntity.SetScore(playerEntity.Score + score);
            if (CheckIfPlayerWin(playerEntity))
            {
                UnityEngine.Debug.Log("Player win");
            }
            else
            {
                SpawnHoop(playerId);
            }
        }

        private void SpawnHoop(Guid playerId)
        {
            BasketballCourtView court = _environmentFactory.GetCourt(playerId);
            Transform hoopSpawnPoint = court.SelectRandomHoopSpawnPoint();

            _hoopFactory.CreateHoop(hoopSpawnPoint);
        }
        
        private bool CheckIfPlayerWin(PlayerEntity playerEntity)
        {
            bool playerWin = playerEntity.Score >= _gameplayConfiguration.ScoresToWIn;
            return playerWin;
        }
    }
}
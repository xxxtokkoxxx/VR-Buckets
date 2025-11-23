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
        public event Action<Guid> OnGameFinished;

        private readonly IGameplayConfiguration _gameplayConfiguration;
        private readonly IHoopFactory _hoopFactory;
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IPlayersContainer _playersContainer;

        public GameplayProcessor(IGameplayConfiguration gameplayConfiguration, IHoopFactory hoopFactory,
            IEnvironmentFactory environmentFactory, IPlayersContainer playersContainer)
        {
            _gameplayConfiguration = gameplayConfiguration;
            _hoopFactory = hoopFactory;
            _environmentFactory = environmentFactory;
            _playersContainer = playersContainer;
        }

        public void EnrollScore(Guid playerId, int score)
        {
            PlayerEntity playerEntity = _playersContainer.GetPlayer(playerId);

            playerEntity.SetScore(playerEntity.Score + score);
            if (CheckIfPlayerWin(playerEntity))
            {
                Debug.Log("Player win");
                OnGameFinished?.Invoke(playerId);
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

            _hoopFactory.CreateHoop(hoopSpawnPoint, playerId);
        }

        private bool CheckIfPlayerWin(PlayerEntity playerEntity)
        {
            bool playerWin = playerEntity.Score >= _gameplayConfiguration.ScoresToWIn;
            return playerWin;
        }
    }
}
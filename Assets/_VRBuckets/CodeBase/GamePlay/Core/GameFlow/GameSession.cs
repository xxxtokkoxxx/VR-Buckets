using System;
using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Data;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.GamePlay.Player;
using _VRBuckets.CodeBase.Infrastructure.DI;
using _VRBuckets.CodeBase.Network.Player;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public class GameSession : IGameSession
    {
        private bool _isSubscribedOnGameFinished;

        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IHoopFactory _hoopFactory;
        private readonly IMonoBehaviourProvider _monoBehaviourProvider;
        private readonly IPlayersContainer _playersContainer;
        private readonly IGameplayProcessor _gameplayProcessor;
        private readonly IUIService _uiService;
        private readonly IGameResultsContainer _gameResultsContainer;
        private readonly IBallLifecycleSystem _ballLifecycleSystem;
        private readonly IPlayerRigFactory _playerRigFactory;

        public GameSession(IEnvironmentFactory environmentFactory,
            IBallFactory ballFactory, IHoopFactory hoopFactory,
            IMonoBehaviourProvider monoBehaviourProvider,
            IPlayersContainer playersContainer,
            IGameplayProcessor gameplayProcessor,
            IUIService uiService,
            IGameResultsContainer gameResultsContainer,
            IBallLifecycleSystem ballLifecycleSystem,
            IPlayerRigFactory playerRigFactory)
        {
            _environmentFactory = environmentFactory;
            _ballFactory = ballFactory;
            _hoopFactory = hoopFactory;
            _monoBehaviourProvider = monoBehaviourProvider;
            _playersContainer = playersContainer;
            _gameplayProcessor = gameplayProcessor;
            _uiService = uiService;
            _gameResultsContainer = gameResultsContainer;
            _ballLifecycleSystem = ballLifecycleSystem;
            _playerRigFactory = playerRigFactory;
        }

        public void StartGame()
        {
            if (!_isSubscribedOnGameFinished)
            {
                _gameplayProcessor.OnGameFinished += EndGame;
                _isSubscribedOnGameFinished = true;
            }

            InitPlayers();

            foreach (KeyValuePair<int, PlayerEntity> player in _playersContainer.GetPlayers())
            {
                BasketballCourtView court = _environmentFactory.CrateBasketballCourt(
                    _monoBehaviourProvider.UserCameraTransform.transform,
                    _monoBehaviourProvider.UserCameraTransform.transform, player.Value.Id);

                _ballFactory.CreateBall(court.BallSpawnPoint, player.Value.Id);
                _hoopFactory.CreateHoop(court.SelectRandomHoopSpawnPoint(), player.Value.Id);
            }

            _ballLifecycleSystem.SubscribeOnSelectBallActions();
        }

        private void EndGame()
        {
            _gameplayProcessor.OnGameFinished -= EndGame;
            KeyValuePair<int, PlayerEntity> winner = _playersContainer.GetPlayers().OrderByDescending(a=>a.Value.Score).First();

            _gameResultsContainer.SetGameResults(new GameResults
            {
                Scores = winner.Value.Score,
                WinnerId = winner.Value.Id,
                WinnerName = winner.Value.Name,
            });

            _isSubscribedOnGameFinished = false;
            _uiService.Show(ViewType.GameOver);
            _ballLifecycleSystem.CleanUpListeners();
        }

        private void InitPlayers()
        {
            //if single player
            //else
            foreach (KeyValuePair<int, PlayerEntity> player in _playersContainer.GetPlayers())
            {
                _playerRigFactory.CreateNetworkRig(Vector3.zero);
            }
        }
    }
}
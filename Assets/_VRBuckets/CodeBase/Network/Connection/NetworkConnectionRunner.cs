using System.Threading;
using _VRBuckets.CodeBase.GamePlay.Player;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Logging;
using _VRBuckets.CodeBase.Network.Configuration;
using _VRBuckets.CodeBase.Network.Messaging;
using _VRBuckets.CodeBase.Network.Messaging.NetworkEvents;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Photon.Realtime;
using UnityEngine;

namespace _VRBuckets.CodeBase.Network.Connection
{
    public class NetworkConnectionRunner : INetworkConnectionRunner
    {
        private NetworkRunner _networkRunner;

        private readonly INetworkRunnerCallbacks _networkRunnerCallbacks;
        private readonly INetworkConfigurationProvider _networkConfigurationProvider;
        private readonly INetworkEventBus _networkEventBus;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlayersContainer _playersContainer;
        private int _playersCount;

        public NetworkConnectionRunner(INetworkRunnerCallbacks networkRunnerCallbacks,
            INetworkConfigurationProvider networkConfigurationProvider,
            INetworkEventBus networkEventBus,
            IGameStateMachine gameStateMachine,
            IPlayersContainer playersContainer)
        {
            _networkRunnerCallbacks = networkRunnerCallbacks;
            _networkConfigurationProvider = networkConfigurationProvider;
            _networkEventBus = networkEventBus;
            _gameStateMachine = gameStateMachine;
            _playersContainer = playersContainer;
        }

        public NetworkRunner NetworkRunner => _networkRunner;

        public void Initialize(NetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
            _networkRunner.AddCallbacks(_networkRunnerCallbacks);

            _networkEventBus.Subscribe<PlayerJoinedEvent>(PlayerConnected);
            _networkEventBus.Subscribe<PlayerDisconnectedEvent>(PlayerDisconnected);
        }

        public async UniTask Connect(GameMode gameMode, CancellationToken token)
        {
            _playersCount = 1;

            int playersCount = gameMode == GameMode.Single
                ? _playersCount
                : _networkConfigurationProvider.MaxPlayersCountPerRoom;

            StartGameArgs gameArgs = new StartGameArgs
            {
                GameMode = gameMode,
                PlayerCount = playersCount,
                MatchmakingMode = MatchmakingMode.FillRoom,
                StartGameCancellationToken = token
            };

            await _networkRunner.StartGame(gameArgs);
        }

        public void Dispose()
        {
            _networkEventBus.Unsubscribe<PlayerJoinedEvent>(PlayerConnected);
            _networkEventBus.Unsubscribe<PlayerDisconnectedEvent>(PlayerDisconnected);
        }

        private void PlayerConnected(PlayerJoinedEvent networkEvent)
        {
            AppLogger.Log(LogCategory.Network, "Player joined");
            Debug.Log("network runner session " + _networkRunner.SessionInfo.PlayerCount + " connected");
            _gameStateMachine.Enter<GamePreparationState>();
            _playersContainer.AddPlayer(new PlayerEntity($"Player {networkEvent.Player.PlayerId}",
                networkEvent.Player.PlayerId, networkEvent.Player));
        }

        private void PlayerDisconnected(PlayerDisconnectedEvent networkEvent)
        {
            AppLogger.Log(LogCategory.Network, "Player disconnected");
            Debug.Log("network runner session " + _networkRunner.SessionInfo.PlayerCount + " disconnected");
            _playersContainer.RemovePlayer(networkEvent.Player.PlayerId);
        }
    }
}
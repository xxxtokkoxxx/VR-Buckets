using System.Threading;
using _VRBuckets.CodeBase.Network.Configuration;
using Cysharp.Threading.Tasks;
using Fusion;
using Fusion.Photon.Realtime;

namespace _VRBuckets.CodeBase.Network.Connection
{
    public class NetworkConnectionRunner : INetworkConnectionRunner
    {
        private NetworkRunner _networkRunner;

        private readonly INetworkRunnerCallbacks _networkRunnerCallbacks;
        private readonly INetworkConfigurationProvider _networkConfigurationProvider;

        public NetworkConnectionRunner(INetworkRunnerCallbacks networkRunnerCallbacks,
            INetworkConfigurationProvider networkConfigurationProvider)
        {
            _networkRunnerCallbacks = networkRunnerCallbacks;
            _networkConfigurationProvider = networkConfigurationProvider;
        }

        public void Initialize(NetworkRunner networkRunner)
        {
            _networkRunner = networkRunner;
            _networkRunner.AddCallbacks(_networkRunnerCallbacks);
        }

        public async UniTask Connect(CancellationToken token)
        {
            StartGameArgs gameArgs = new StartGameArgs
            {
                GameMode = GameMode.AutoHostOrClient,
                PlayerCount = _networkConfigurationProvider.MaxPlayersCountPerRoom,
                MatchmakingMode = MatchmakingMode.FillRoom,
                StartGameCancellationToken = token
            };

            await _networkRunner.StartGame(gameArgs);
        }
    }
}
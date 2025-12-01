using _VRBuckets.CodeBase.Configuration;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Core.GameFlow;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Network.Player;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Network.Connection
{
    public class GamePreparationState : IState
    {
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IHoopFactory _hoopFactory;
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIService _uiService;
        private readonly IGameplayConfiguration _gameplayConfiguration;
        private readonly IPlayerRigFactory _playerRigFactory;

        public GamePreparationState(IEnvironmentFactory environmentFactory,
            IBallFactory ballFactory,
            IHoopFactory hoopFactory,
            IGameStateMachine stateMachine,
            IUIService uiService,
            IGameplayConfiguration gameplayConfiguration,
            IPlayerRigFactory playerRigFactory)
        {
            _environmentFactory = environmentFactory;
            _ballFactory = ballFactory;
            _hoopFactory = hoopFactory;
            _stateMachine = stateMachine;
            _uiService = uiService;
            _gameplayConfiguration = gameplayConfiguration;
            _playerRigFactory = playerRigFactory;
        }

        public async UniTask Enter(object payload)
        {
            _uiService.HideAll();
            await LoadEnvironment();
            _stateMachine.Enter<GameState>();
        }

        private async UniTask LoadEnvironment()
        {
            UniTask[] tasks =
            {
                _environmentFactory.LoadEnvironment(),
                _ballFactory.LoadBallReference(),
                _hoopFactory.LoadHoopReference(),
                _gameplayConfiguration.LoadAndSetConfiguration(),
                _playerRigFactory.LoadNetworkRig(),
                LoadGameScene()
            };

            await UniTask.WhenAll(tasks);
        }

        private async UniTask LoadGameScene()
        {
            await NetworkRunnerProvider.NetworkRunner.LoadScene(SceneNames.Game);
        }
    }
}
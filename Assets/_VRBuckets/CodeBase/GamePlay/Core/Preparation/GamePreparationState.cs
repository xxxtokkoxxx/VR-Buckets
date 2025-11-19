using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Core.GameFlow;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.GamePlay.Core.Preparation
{
    public class GamePreparationState : IState
    {
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IHoopFactory _hoopFactory;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IGameStateMachine _stateMachine;

        public GamePreparationState(IEnvironmentFactory environmentFactory, IBallFactory ballFactory,
            IHoopFactory hoopFactory, ISceneLoaderService sceneLoaderService, IGameStateMachine stateMachine)
        {
            _environmentFactory = environmentFactory;
            _ballFactory = ballFactory;
            _hoopFactory = hoopFactory;
            _sceneLoaderService = sceneLoaderService;
            _stateMachine = stateMachine;
        }

        public async void Enter(object payload)
        {
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
                _sceneLoaderService.LoadScene(SceneNames.Game)
            };

            await UniTask.WhenAll(tasks);
        }
    }
}
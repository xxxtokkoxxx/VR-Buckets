using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.GamePlay.Player;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Services;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public class GameState : IExitState
    {
        private readonly IGameSession _gameSession;
        private readonly IBallFactory _ballFactory;
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IHoopFactory _hoopFactory;
        private readonly IUIService _uiService;
        private readonly IPlayersContainer _playersContainer;

        public GameState(IGameSession gameSession, IBallFactory ballFactory, IEnvironmentFactory environmentFactory,
            IHoopFactory hoopFactory, IUIService uiService, IPlayersContainer playersContainer)
        {
            _gameSession = gameSession;
            _ballFactory = ballFactory;
            _environmentFactory = environmentFactory;
            _hoopFactory = hoopFactory;
            _uiService = uiService;
            _playersContainer = playersContainer;
        }

        public UniTask Enter(object payload)
        {
            _gameSession.StartGame();
            return UniTask.CompletedTask;
        }

        public UniTask Exit()
        {
            _ballFactory.Release();
            _environmentFactory.Release();
            _hoopFactory.Release();
            _uiService.HideAll();
            _playersContainer.ClearPlayers();

            return UniTask.CompletedTask;
        }
    }
}
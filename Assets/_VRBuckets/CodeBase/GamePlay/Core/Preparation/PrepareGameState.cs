using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.GamePlay.Core.Preparation
{
    public class PrepareGameState : IState
    {
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IHoopFactory _hoopFactory;

        public PrepareGameState(IEnvironmentFactory environmentFactory, IBallFactory ballFactory, IHoopFactory hoopFactory)
        {
            _environmentFactory = environmentFactory;
            _ballFactory = ballFactory;
            _hoopFactory = hoopFactory;
        }

        public async void Enter(object payload)
        {
            await LoadEnvironment();
        }

        private async UniTask LoadEnvironment()
        {
            UniTask[] tasks =
            {
                _environmentFactory.LoadEnvironment(),
                _ballFactory.LoadBallReference(),
                _hoopFactory.LoadHoopReference(),
            };

            await UniTask.WhenAll(tasks);
        }
    }
}
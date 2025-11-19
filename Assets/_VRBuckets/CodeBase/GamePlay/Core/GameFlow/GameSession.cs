using System.Collections.Generic;
using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.GamePlay.Player;

namespace _VRBuckets.CodeBase.GamePlay.Core.GameFlow
{
    public class GameSession
    {
        private List<PlayerEntity> _players = new();

        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IHoopFactory _hoopFactory;

        public GameSession(IEnvironmentFactory environmentFactory, IBallFactory ballFactory, IHoopFactory hoopFactory)
        {
            _environmentFactory = environmentFactory;
            _ballFactory = ballFactory;
            _hoopFactory = hoopFactory;
        }

        public void StartGame()
        {

        }

        private void AddPlayers()
        {

        }

        private void CrateEnvironment()
        {

        }

        private void CreateBall()
        {

        }

        private void CreateHoop()
        {

        }
    }
}
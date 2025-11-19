using UnityEngine;
using VContainer.Unity;

namespace _VRBuckets.CodeBase.GamePlay.Ball
{
    public class BallLifecycleSystem : ITickable
    {
        private readonly IBallFactory _ballFactory;

        public BallLifecycleSystem(IBallFactory ballFactory)
        {
            _ballFactory = ballFactory;
        }

        public void SetLifecycle(GameObject a)
        {
        }

        public void Tick()
        {
            foreach (BallView ball in _ballFactory.GetCreatedBalls())
            {
                if(ball)
            }
        }
    }
}
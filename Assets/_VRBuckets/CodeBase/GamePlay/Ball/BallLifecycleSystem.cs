using UnityEngine;
using VContainer.Unity;

namespace _VRBuckets.CodeBase.GamePlay.Ball
{
    public class BallLifecycleSystem : ITickable, IBallLifecycleSystem
    {
        private float _timeToResetBall;
        private readonly IBallFactory _ballFactory;

        public BallLifecycleSystem(IBallFactory ballFactory)
        {
            _ballFactory = ballFactory;
        }

        public void Tick()
        {
            foreach (BallView ball in _ballFactory.GetCreatedBalls())
            {
                if (ball.BallState == BallState.NotControlled)
                {
                    ball.ReleasedTime += Time.deltaTime;

                    if (ball.ReleasedTime > _timeToResetBall)
                    {
                        ball.SetBallState(BallState.Idle);
                    }
                }
            }
        }

        public void SubscribeOnSelectBallActions()
        {
            foreach (BallView ballView in _ballFactory.GetCreatedBalls())
            {
                ballView.Interactable.selectEntered.AddListener(_ => BallSelectEnter(ballView));
                ballView.Interactable.selectExited.AddListener(_ => BallSelectExit(ballView));
            }
        }

        private void BallSelectEnter(BallView ballView)
        {
            ballView.SetBallState(BallState.Controlled);
        }

        private void BallSelectExit(BallView ballView)
        {
            ballView.SetBallState(BallState.NotControlled);
        }
    }
}
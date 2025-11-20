using System;
using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Core.GameFlow;
using UnityEngine;
using VContainer;

namespace _VRBuckets.CodeBase.GamePlay.Bucket
{
    public class HoopView : MonoBehaviour
    {
        private Guid _playerId;
        private IGameplayProcessor _gameplayProcessor;
        private int _minScoreValue = 1;

        [Inject]
        public void Inject(IGameplayProcessor gameplayProcessor)
        {
            _gameplayProcessor = gameplayProcessor;
        }

        public void Initialize(Guid playerId)
        {
            playerId = playerId;
        }

        private void OnTriggerEnter(Collider other)
        {
            bool componentExists = other.TryGetComponent(out BallView ballView);

            if (componentExists && ballView.PlayerId == _playerId)
            {
                _gameplayProcessor.EnrollScore(_playerId, _minScoreValue);
            }
        }
    }
}
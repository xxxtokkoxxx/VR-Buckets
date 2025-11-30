using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Core.GameFlow;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace _VRBuckets.CodeBase.GamePlay.Bucket
{
    public class HoopView : MonoBehaviour
    {
        private int _playerId;
        private IGameplayProcessor _gameplayProcessor;
        private int _minScoreValue = 1;

        public int PlayerId => _playerId;

        [Inject]
        public void Inject(IGameplayProcessor gameplayProcessor)
        {
            _gameplayProcessor = gameplayProcessor;
        }

        public void Initialize(int playerId)
        {
            _playerId = playerId;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger enter " + other.gameObject.name);
            bool componentExists = other.gameObject.TryGetComponent(out BallView ballView);

            if (componentExists && ballView.PlayerId == _playerId)
            {
                _gameplayProcessor.EnrollScore(_playerId, _minScoreValue);
            }
        }

#if UNITY_EDITOR

        [Button]
        private void EnrollScore()
        {
            _gameplayProcessor.EnrollScore(_playerId, _minScoreValue);
        }

        [Button]
        private void EnrollMaxScore()
        {
            _gameplayProcessor.EnrollScore(_playerId, int.MaxValue);
        }
#endif
    }
}
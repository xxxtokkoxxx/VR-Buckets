using System;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Bucket
{
    public class HoopView : MonoBehaviour
    {
        public Guid PlayerId;
        public event Action OnBallScored;

        public void Initialize(Guid playerId)
        {
            playerId = playerId;
        }

        private void OnTriggerEnter(Collider other)
        {
            bool componentExists = other.TryGetComponent(out BallView ballView);

            if (componentExists && ballView.PlayerId == PlayerId)
            {
                OnBallScored?.Invoke();
            }
        }
    }
}
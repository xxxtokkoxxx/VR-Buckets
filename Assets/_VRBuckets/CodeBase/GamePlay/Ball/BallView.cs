using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace _VRBuckets.CodeBase.GamePlay.Ball
{
    public class BallView : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        
        [SerializeField] private XRBaseInteractable _interactable;

        public int BounceCount { get; private set; }
        public Guid PlayerId { get; private set; }
        public float ReleasedTime { get; set; }
        public BallState BallState { get; private set; }
        public XRBaseInteractable Interactable => _interactable;

        public void Initialize(Guid playerId)
        {
            PlayerId = playerId;
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }

        public void SetBallState(BallState ballState)
        {
            BallState = ballState;
            
            switch (ballState)
            {
                case BallState.Idle:
                    ResetPosition();
                    break;
                case BallState.Controlled:
                    ReleasedTime = 0;
                    break;
            }
        }

        public void ResetPosition()
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
        }

        public void ResetBounceCount()
        {
            BounceCount = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            BounceCount++;
        }
    }
}
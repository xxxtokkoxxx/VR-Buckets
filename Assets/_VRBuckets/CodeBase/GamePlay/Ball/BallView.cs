using System;
using Fusion;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace _VRBuckets.CodeBase.GamePlay.Ball
{
    public class BallView : SimulationBehaviour
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        [SerializeField] private XRBaseInteractable _interactable;
        [SerializeField] private Rigidbody _rigidbody;

        public int BounceCount { get; private set; }
        public int PlayerId { get; private set; }
        public float ReleasedTime { get; set; }
        public BallState BallState { get; private set; }
        public XRBaseInteractable Interactable => _interactable;

        public void Initialize(int playerId)
        {
            PlayerId = playerId;
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }

        [Button]
        public void SetBallState(BallState ballState)
        {
            BallState = ballState;
            Debug.Log("set ball state " + ballState);
            switch (ballState)
            {
                case BallState.Idle:
                    ResetPosition();
                    // _rigidbody.Sleep();
                    break;
                case BallState.Controlled:
                    ReleasedTime = 0;
                    // _rigidbody.Sleep();
                    break;
                case BallState.NotControlled:
                    // _rigidbody.WakeUp();
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
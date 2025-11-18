using System;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Environment
{
    public class BasketballCourtView : MonoBehaviour
    {
        public Guid PlayerId { get; private set; }
        public Transform PlayerInitPoint => _playerInitPoint;
        public Transform BallSpawnPoint => _ballSpawnPoint;
        public Transform[] HoopSpawnPoints => _hoopSpawnPoints;

        [SerializeField] private Transform _playerInitPoint;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private Transform[] _hoopSpawnPoints;

        public void Initialize(Guid playerId)
        {
            PlayerId = playerId;
        }
    }
}
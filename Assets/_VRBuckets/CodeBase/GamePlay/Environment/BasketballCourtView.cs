using System;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace _VRBuckets.CodeBase.GamePlay.Environment
{
    public class BasketballCourtView : MonoBehaviour
    {
        public int PlayerId { get; private set; }
        public Transform PlayerInitPoint => _playerInitPoint;
        public Transform BallSpawnPoint => _ballSpawnPoint;
        public Transform[] HoopSpawnPoints => _hoopSpawnPoints;

        [SerializeField] private Transform _playerInitPoint;
        [SerializeField] private Transform _ballSpawnPoint;
        [SerializeField] private Transform[] _hoopSpawnPoints;

        private int _currentHoopSpawnIndex = -1;

        public void Initialize(int playerId)
        {
            PlayerId = playerId;
        }

        public Transform SelectRandomHoopSpawnPoint()
        {
            if (_currentHoopSpawnIndex == -1)
            {
                _currentHoopSpawnIndex = UnityEngine.Random.Range(0, HoopSpawnPoints.Length);
            }
            else
            {
                var index = UnityEngine.Random.Range(0, HoopSpawnPoints.Length);
                if (index == _currentHoopSpawnIndex)
                {
                    if (_currentHoopSpawnIndex == _hoopSpawnPoints.Length - 1)
                    {
                        _currentHoopSpawnIndex--;
                    }
                    else
                    {
                        _currentHoopSpawnIndex++;
                    }
                }
                else
                {
                    _currentHoopSpawnIndex = index;
                }
            }

            Transform hoop = _hoopSpawnPoints[_currentHoopSpawnIndex];

            return hoop;
        }
    }
}
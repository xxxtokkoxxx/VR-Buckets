using UnityEngine;

namespace _VRBuckets.CodeBase.Data
{
    [CreateAssetMenu(fileName = "GameConfiguration", menuName = "VR-Buckets/GameConfiguration", order = 0)]
    public class GameConfigSO : ScriptableObject
    {
        public float MatchTime;
        public int ScoresToWin;
    }
}
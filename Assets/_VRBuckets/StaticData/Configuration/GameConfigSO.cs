using UnityEngine;

namespace _VRBuckets.StaticData.Configuration
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig", order = 0)]
    public class GameConfigSO : ScriptableObject
    {
        public float MatchTime;
        public int ScoresToWin;
    }
}
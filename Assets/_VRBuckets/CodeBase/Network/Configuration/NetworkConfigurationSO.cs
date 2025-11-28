using UnityEngine;

namespace _VRBuckets.CodeBase.Network.Configuration
{
    [CreateAssetMenu(fileName = "NetworkConfiguration", menuName = "VR-Buckets/Network/Configuration", order = 0)]
    public class NetworkConfigurationSO : ScriptableObject
    {
        public int MaxPlayersCountPerRoom;
    }
}
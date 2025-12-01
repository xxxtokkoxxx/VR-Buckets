using Fusion;
using UnityEngine;

namespace _VRBuckets.CodeBase.Network.Connection
{
    // Singleton here as the NetworkRunner has to be destroyed after disconnection from the game
    // https://doc.photonengine.com/fusion/current/manual/network-runner
    public class NetworkRunnerProvider
    {
        private static NetworkRunner _networkRunner;
        public static NetworkRunner NetworkRunner
        {
            get
            {
                if (_networkRunner == null)
                {
                    _networkRunner = new GameObject().AddComponent<NetworkRunner>();
                    _networkRunner.MakeDontDestroyOnLoad(_networkRunner.gameObject);
                }

                return _networkRunner;
            }
        }

        public static void DestroyNetworkRunner()
        {
            Object.Destroy(_networkRunner);
        }
    }
}
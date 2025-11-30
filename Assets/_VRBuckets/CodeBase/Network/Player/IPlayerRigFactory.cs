using Cysharp.Threading.Tasks;
using Fusion.XR.Shared.Core;
using UnityEngine;

namespace _VRBuckets.CodeBase.Network.Player
{
    public interface IPlayerRigFactory
    {
        UniTask LoadNetworkRig();
        NetworkRig CreateNetworkRig(Vector3 position);
        void Release();
    }
}
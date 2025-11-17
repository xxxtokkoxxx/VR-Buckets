using UnityEngine;

namespace _VRBuckets.CodeBase.Infrastructure.DI
{
    public interface IMonoBehaviourProvider
    {
        Transform UIViewsParent { get; }
        Camera UserCameraTransform { get; }
    }
}
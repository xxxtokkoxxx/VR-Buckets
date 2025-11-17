using UnityEngine;

namespace _VRBuckets.CodeBase.Infrastructure.DI
{
    public class MonoBehavioursProvider : MonoBehaviour, IMonoBehaviourProvider
    {
        [SerializeField] private Transform _uiViewsParent;
        [SerializeField] private Camera _userMainCamera;
        public Transform UIViewsParent => _uiViewsParent;

        public Camera UserCameraTransform => _userMainCamera;
    }
}
using UnityEngine;

namespace _VRBuckets.CodeBase.UI
{
    public abstract class BaseUiController<TView> : IViewController where TView : IView
    {
        public abstract ViewType ViewType { get; }
        public abstract void Show();
        public abstract void Hide();

        protected TView View;

        protected void PlaceViewInFrontOfTarget(Transform target)
        {
            Transform cameraTransform = target;

            View.GameObject.transform.position = cameraTransform.position + cameraTransform.forward;
            View.GameObject.transform.LookAt(cameraTransform);
            View.GameObject.transform.rotation *= Quaternion.Euler(0, 180, 0);
        }
    }
}
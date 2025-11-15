using System.Linq;
using _VRBuckets.CodeBase.Debug;
using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Services
{
    public class UIService : IUIService
    {
        private IViewController[] _controllers;

        public void Initialize(IViewController[] viewControllers)
        {
            _controllers = viewControllers;
        }

        public void Show(ViewType viewType)
        {
            IViewController controller = GetUiController(viewType);
            controller.Show();
        }

        public void Hide(ViewType viewType)
        {
            IViewController controller = GetUiController(viewType);
            controller.Hide();
        }

        private IViewController GetUiController(ViewType viewType)
        {
            IViewController controller = _controllers.FirstOrDefault(a=>a.ViewType == viewType);

            if (controller == null)
            {
                AppLogger.LogError(LogCategory.UI, $"No controller found for view type {viewType}");
                return null;
            }

            return controller;
        }
    }
}
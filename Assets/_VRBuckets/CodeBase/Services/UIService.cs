using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Logging;
using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Services
{
    public class UIService : IUIService
    {
        private IViewController[] _controllers;
        private List<IViewController> _activeControllers = new();

        public void Initialize(IViewController[] viewControllers)
        {
            _controllers = viewControllers;
        }

        public void Show(ViewType viewType)
        {
            IViewController controller = GetUiController(viewType);
            _activeControllers.Add(controller);
            controller.Show();
        }

        public void Hide(ViewType viewType)
        {
            IViewController controller = GetUiController(viewType);
            _activeControllers.Remove(controller);
            controller.Hide();
        }

        public void HideAll()
        {
            foreach (IViewController controller in _activeControllers)
            {
                controller.Hide();
            }

            _activeControllers.Clear();
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
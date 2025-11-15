using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class StatesFactory : IStatesFactory
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IUIViewsFactory _uiViewsFactory;
        private readonly IUIService _uiService;
        private readonly IViewController[] _viewControllers;

        public StatesFactory(ISceneLoaderService sceneLoaderService,
            IUIViewsFactory uiViewsFactory,
            IUIService uiService,
            IEnumerable<IViewController> viewControllers)
        {
            _sceneLoaderService = sceneLoaderService;
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
            _viewControllers = (IViewController[]) viewControllers;
        }

        public Dictionary<Type, IState> CreateStates()
        {
            IState bootstrapState = new BootstrapState(_sceneLoaderService, _uiViewsFactory, _uiService, _viewControllers);
            IState menuState = new MainMenuState();

            Dictionary<Type, IState> states = new Dictionary<Type, IState>()
            {
                {typeof(BootstrapState), bootstrapState},
                {typeof(MainMenuState), menuState}
            };

            return states;
        }
    }
}
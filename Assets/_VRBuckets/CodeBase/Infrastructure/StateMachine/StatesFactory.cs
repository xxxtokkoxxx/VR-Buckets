using System;
using System.Collections.Generic;
using _VRBuckets.CodeBase.GamePlay.Ball;
using _VRBuckets.CodeBase.GamePlay.Bucket;
using _VRBuckets.CodeBase.GamePlay.Core.Preparation;
using _VRBuckets.CodeBase.GamePlay.Environment;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class StatesFactory : IStatesFactory
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IUIViewsFactory _uiViewsFactory;
        private readonly IUIService _uiService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IEnvironmentFactory _environmentFactory;
        private readonly IBallFactory _ballFactory;
        private readonly IHoopFactory _hoopFactory;
        private readonly IViewController[] _viewControllers;

        public StatesFactory(ISceneLoaderService sceneLoaderService,
            IUIViewsFactory uiViewsFactory,
            IUIService uiService,
            IGameStateMachine gameStateMachine,
            IEnvironmentFactory environmentFactory,
            IBallFactory ballFactory,
            IHoopFactory hoopFactory,
            IEnumerable<IViewController> viewControllers)
        {
            _sceneLoaderService = sceneLoaderService;
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
            _gameStateMachine = gameStateMachine;
            _environmentFactory = environmentFactory;
            _ballFactory = ballFactory;
            _hoopFactory = hoopFactory;
            _viewControllers = (IViewController[])viewControllers;
        }

        public Dictionary<Type, IState> CreateStates()
        {
            IState bootstrapState =
                new BootstrapState(_uiViewsFactory, _uiService, _viewControllers, _gameStateMachine);
            IState menuState = new MainMenuState(_sceneLoaderService, _uiService);
            IState prepareGameState =
                new GamePreparationState(_environmentFactory, _ballFactory, _hoopFactory, _sceneLoaderService);

            Dictionary<Type, IState> states = new Dictionary<Type, IState>()
            {
                { typeof(BootstrapState), bootstrapState },
                { typeof(MainMenuState), menuState },
                { typeof(GamePreparationState), prepareGameState }
            };

            return states;
        }
    }
}
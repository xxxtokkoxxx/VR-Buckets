using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IUIViewsFactory _uiViewsFactory;
        private readonly IUIService _uiService;
        private readonly IViewController[] _viewControllers;

        public BootstrapState(ISceneLoaderService sceneLoaderService,
            IUIViewsFactory uiViewsFactory,
            IUIService uiService,
            IViewController[] viewControllers)
        {
            _sceneLoaderService = sceneLoaderService;
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
            _viewControllers = viewControllers;
        }

        public async void Enter(object payload)
        {
            _uiService.Initialize(_viewControllers);
            await _uiViewsFactory.LoadViews();
            _sceneLoaderService.LoadScene(SceneNames.MainMenu);
        }
    }
}
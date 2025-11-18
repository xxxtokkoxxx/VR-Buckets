using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly IUIViewsFactory _uiViewsFactory;
        private readonly IUIService _uiService;
        private readonly IViewController[] _viewControllers;
        private readonly IGameStateMachine _stateMachine;

        public BootstrapState(IUIViewsFactory uiViewsFactory,
            IUIService uiService,
            IViewController[] viewControllers,
            IGameStateMachine stateMachine)
        {
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
            _viewControllers = viewControllers;
            _stateMachine = stateMachine;
        }

        public async void Enter(object payload)
        {
            _uiService.Initialize(_viewControllers);
            await _uiViewsFactory.LoadViews();

            _stateMachine.Enter<MainMenuState>();
        }
    }
}
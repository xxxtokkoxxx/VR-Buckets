using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Network.Configuration;
using _VRBuckets.CodeBase.Services;
using _VRBuckets.CodeBase.UI;
using Cysharp.Threading.Tasks;

namespace _VRBuckets.CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly IUIViewsFactory _uiViewsFactory;
        private readonly IUIService _uiService;
        private readonly IEnumerable<IViewController> _viewControllers;
        private readonly IGameStateMachine _stateMachine;
        private readonly INetworkConfigurationProvider _networkConfigurationProvider;

        public BootstrapState(IUIViewsFactory uiViewsFactory,
            IUIService uiService,
            IEnumerable<IViewController> viewControllers,
            IGameStateMachine stateMachine,
            INetworkConfigurationProvider networkConfigurationProvider)
        {
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
            _viewControllers = viewControllers;
            _stateMachine = stateMachine;
            _networkConfigurationProvider = networkConfigurationProvider;
        }

        public async UniTask Enter(object payload)
        {
            _uiService.Initialize(_viewControllers.ToArray());

            await LoadStaticData();

            _stateMachine.Enter<MainMenuState>();
        }

        private async UniTask LoadStaticData()
        {
            UniTask[] tasks =
            {
                _uiViewsFactory.LoadViews(),
                _networkConfigurationProvider.LoadAndSetConfiguration()
            };

            await UniTask.WhenAll(tasks);
        }
    }
}
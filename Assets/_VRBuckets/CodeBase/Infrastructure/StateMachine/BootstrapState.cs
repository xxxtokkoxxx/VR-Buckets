using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Infrastructure.DI;
using _VRBuckets.CodeBase.Network.Configuration;
using _VRBuckets.CodeBase.Network.Connection;
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
        private readonly INetworkConnectionRunner _networkConnectionRunner;
        private readonly IMonoBehaviourProvider _monoBehaviourProvider;
        private readonly INetworkConfigurationProvider _networkConfigurationProvider;

        public BootstrapState(IUIViewsFactory uiViewsFactory,
            IUIService uiService,
            IEnumerable<IViewController> viewControllers,
            IGameStateMachine stateMachine,
            INetworkConnectionRunner networkConnectionRunner,
            IMonoBehaviourProvider monoBehaviourProvider,
            INetworkConfigurationProvider networkConfigurationProvider)
        {
            _uiViewsFactory = uiViewsFactory;
            _uiService = uiService;
            _viewControllers = viewControllers;
            _stateMachine = stateMachine;
            _networkConnectionRunner = networkConnectionRunner;
            _monoBehaviourProvider = monoBehaviourProvider;
            _networkConfigurationProvider = networkConfigurationProvider;
        }

        public async UniTask Enter(object payload)
        {
            _uiService.Initialize(_viewControllers.ToArray());
            _networkConnectionRunner.Initialize(_monoBehaviourProvider.NetworkRunner);

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
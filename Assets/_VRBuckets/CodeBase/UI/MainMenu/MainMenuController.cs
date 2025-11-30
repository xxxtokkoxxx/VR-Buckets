using System;
using System.Threading;
using _VRBuckets.CodeBase.GamePlay.Core.Preparation;
using _VRBuckets.CodeBase.Infrastructure.DI;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.Logging;
using _VRBuckets.CodeBase.Network.Connection;
using Cysharp.Threading.Tasks;
using Fusion;

namespace _VRBuckets.CodeBase.UI.MainMenu
{
    public class MainMenuController : BaseUiController<MainMenuView>, IViewController
    {
        private bool _subscribed;

        private MainMenuCallbacks _callbacks;
        private CancellationTokenSource _cancellationToken = new();

        private readonly IUIViewsFactory _viewsFactory;
        private readonly IMonoBehaviourProvider _monoBehaviourProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly INetworkConnectionRunner _networkConnectionRunner;

        public MainMenuController(IUIViewsFactory viewsFactory,
            IMonoBehaviourProvider monoBehaviourProvider,
            IGameStateMachine gameStateMachine,
            INetworkConnectionRunner networkConnectionRunner)
        {
            _viewsFactory = viewsFactory;
            _monoBehaviourProvider = monoBehaviourProvider;
            _gameStateMachine = gameStateMachine;
            _networkConnectionRunner = networkConnectionRunner;
        }

        public override ViewType ViewType => ViewType.MainMenu;

        public override void Show()
        {
            Subscribe();

            if (View == null)
            {
                View = _viewsFactory.CreateView<MainMenuView>(ViewType.MainMenu);
                View.Initialize(_callbacks);
            }

            PlaceViewInFrontOfTarget(_monoBehaviourProvider.UserCameraTransform.transform);
        }

        public override void Hide()
        {
            Unsubscribe();
            _viewsFactory.DestroyView(View.Id);
        }

        private void Subscribe()
        {
            if (_subscribed)
            {
                return;
            }

            _subscribed = true;

            if (_callbacks == null)
            {
                _callbacks = new MainMenuCallbacks();
            }

            _callbacks.OnStartMultiPlayer += OnStartMultiPlayer;
            _callbacks.OnStartSinglePlayer += OnStartSinglePlayer;
        }

        private void Unsubscribe()
        {
            _callbacks.OnStartMultiPlayer -= OnStartMultiPlayer;
            _callbacks.OnStartSinglePlayer -= OnStartSinglePlayer;
            _subscribed = false;
        }

        private async void OnStartSinglePlayer()
        {
            await ConnectGame(GameMode.Single);
        }

        private async void OnStartMultiPlayer()
        {
            await ConnectGame(GameMode.AutoHostOrClient);
        }

        private async UniTask ConnectGame(GameMode gameMode)
        {
            try
            {
                ShowSearchingSessionPanel(true);
                await _networkConnectionRunner.Connect(gameMode, _cancellationToken.Token);
                ShowSearchingSessionPanel(false);
            }
            catch (Exception e)
            {
                AppLogger.LogError(LogCategory.Network, e.Message);
            }
            finally
            {
                _cancellationToken.Dispose();
            }
        }

        private void ShowSearchingSessionPanel(bool isEnabled)
        {
            View.SetSearchingSessionPanelEnabled(isEnabled);
        }
    }
}
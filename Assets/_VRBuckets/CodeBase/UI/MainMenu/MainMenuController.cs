using _VRBuckets.CodeBase.GamePlay.Core.Preparation;
using _VRBuckets.CodeBase.Infrastructure.DI;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using UnityEngine;

namespace _VRBuckets.CodeBase.UI.MainMenu
{
    public class MainMenuController : BaseUiController<MainMenuView>, IViewController
    {
        private MainMenuCallbacks _callbacks;
        private readonly IUIViewsFactory _viewsFactory;
        private readonly IMonoBehaviourProvider _monoBehaviourProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private bool _subscribed;

        public MainMenuController(IUIViewsFactory viewsFactory,
            IMonoBehaviourProvider monoBehaviourProvider,
            IGameStateMachine gameStateMachine)
        {
            _viewsFactory = viewsFactory;
            _monoBehaviourProvider = monoBehaviourProvider;
            _gameStateMachine = gameStateMachine;
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

        private void OnStartSinglePlayer()
        {
            _gameStateMachine.Enter<GamePreparationState>();
        }

        private void OnStartMultiPlayer()
        {
            Debug.Log("OnStartMultiPlayer");
        }
    }
}
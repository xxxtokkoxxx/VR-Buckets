using _VRBuckets.CodeBase.GamePlay.Data;
using _VRBuckets.CodeBase.Infrastructure.DI;
using _VRBuckets.CodeBase.Infrastructure.StateMachine;
using _VRBuckets.CodeBase.UI.MainMenu;
using UnityEngine;

namespace _VRBuckets.CodeBase.UI.GameOver
{
    public class GameOverController : BaseUiController<GameOverView>
    {
        private GameOverViewCallbacks _callbacks;
        private readonly IUIViewsFactory _viewsFactory;
        private readonly IMonoBehaviourProvider _monoBehaviourProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IGameResultsContainer _gameResultsContainer;
        private bool _subscribed;

        public GameOverController(IUIViewsFactory viewsFactory,
            IMonoBehaviourProvider monoBehaviourProvider,
            IGameStateMachine gameStateMachine,
            IGameResultsContainer gameResultsContainer)
        {
            _viewsFactory = viewsFactory;
            _monoBehaviourProvider = monoBehaviourProvider;
            _gameStateMachine = gameStateMachine;
            _gameResultsContainer = gameResultsContainer;
        }

        public override ViewType ViewType => ViewType.GameOver;

        public override void Show()
        {
            Subscribe();

            if (View == null)
            {
                View = _viewsFactory.CreateView<GameOverView>(ViewType.GameOver);
                View.Initialize(_callbacks);
            }

            PlaceViewInFrontOfTarget(_monoBehaviourProvider.UserCameraTransform.transform);

            GameResults gameResults = _gameResultsContainer.GetGameResults();
            View.SetGameResultsText(gameResults.WinnerName, gameResults.Scores);
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
                _callbacks = new GameOverViewCallbacks();
            }

            _callbacks.OnMainMenuButtonPressed += OnMainMenuButtonPressed;
        }

        private void Unsubscribe()
        {
            _callbacks.OnMainMenuButtonPressed -= OnMainMenuButtonPressed;
            _subscribed = false;
        }

        private void OnMainMenuButtonPressed()
        {
            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}
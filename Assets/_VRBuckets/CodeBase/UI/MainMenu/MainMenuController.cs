namespace _VRBuckets.CodeBase.UI.MainMenu
{
    public class MainMenuController : IViewController
    {
        private MainMenuView _view;
        private MainMenuCallbacks _callbacks;
        private readonly IUIViewsFactory _viewsFactory;
        private bool _subscribed;

        public MainMenuController(IUIViewsFactory viewsFactory)
        {
            _viewsFactory = viewsFactory;
        }

        public ViewType ViewType => ViewType.MainMenu;

        public void Show()
        {
            Subscribe();
            if (_view == null)
            {
                _view = _viewsFactory.CreateView<MainMenuView>(ViewType.MainMenu);
                _view.Initialize(_callbacks);
            }
        }

        public void Hide()
        {
            Unsubscribe();
            _viewsFactory.DestroyView(_view.Id);
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
            _subscribed = true;
        }

        private void OnStartSinglePlayer()
        {
            UnityEngine.Debug.Log("OnStartSinglePlayer");
        }

        private void OnStartMultiPlayer()
        {
            UnityEngine.Debug.Log("OnStartSinglePlayer");
        }
    }
}
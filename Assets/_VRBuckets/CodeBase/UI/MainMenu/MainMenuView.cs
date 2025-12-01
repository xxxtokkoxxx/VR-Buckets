using UnityEngine;

namespace _VRBuckets.CodeBase.UI.MainMenu
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private GameObject _searchingSessionPanel;
        [SerializeField] private GameObject _mainMenuPanel;

        private MainMenuCallbacks _callbacks;

        public override ViewType ViewType => ViewType.MainMenu;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(MainMenuCallbacks callbacks)
        {
            _callbacks = callbacks;
        }

        public void StartSinglePlayer()
        {
            _callbacks.StartSinglePlayer();
        }

        public void StartMultiPlayer()
        {
            _callbacks.StartMultiPlayer();
        }

        public void SetSearchingSessionPanelEnabled(bool isEnabled)
        {
            _searchingSessionPanel.SetActive(isEnabled);
            _mainMenuPanel.SetActive(!isEnabled);
        }

        public void CancelSearchingGame()
        {
            _callbacks.CancelSearchingGame();
        }
    }
}
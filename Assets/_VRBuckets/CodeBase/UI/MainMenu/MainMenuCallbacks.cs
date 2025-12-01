using System;

namespace _VRBuckets.CodeBase.UI.MainMenu
{
    public class MainMenuCallbacks
    {
        public event Action OnStartSinglePlayer;
        public event Action OnStartMultiPlayer;

        public event Action OnCancelSearchingGame;

        public void StartSinglePlayer()
        {
            OnStartSinglePlayer?.Invoke();
        }

        public void StartMultiPlayer()
        {
            OnStartMultiPlayer?.Invoke();
        }

        public void CancelSearchingGame()
        {
            OnCancelSearchingGame?.Invoke();
        }
    }
}
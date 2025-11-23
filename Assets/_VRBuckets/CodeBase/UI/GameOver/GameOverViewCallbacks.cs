using System;

namespace _VRBuckets.CodeBase.UI.GameOver
{
    public class GameOverViewCallbacks
    {
        public event Action OnMainMenuButtonPressed;

        public void MainMenuButtonPressed()
        {
            OnMainMenuButtonPressed?.Invoke();
        }
    }
}
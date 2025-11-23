using TMPro;
using UnityEngine;

namespace _VRBuckets.CodeBase.UI.GameOver
{
    public class GameOverView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _gameResultText;

        private GameOverViewCallbacks _callbacks;
        public override ViewType ViewType => ViewType.GameOver;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(GameOverViewCallbacks callbacks)
        {
            _callbacks = callbacks;
        }

        public void SetGameResultsText(string playerName, int scores)
        {
            _gameResultText.text = $"{playerName} has won the game with score {scores}!";
        }

        public void OnMainMenuButtonPressed()
        {
            _callbacks.MainMenuButtonPressed();
        }
    }
}
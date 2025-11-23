namespace _VRBuckets.CodeBase.GamePlay.Data
{
    public class GameResultsContainer : IGameResultsContainer
    {
        private GameResults _gameResults;

        public void SetGameResults(GameResults gameResults)
        {
            _gameResults = gameResults;
        }

        public GameResults GetGameResults() => _gameResults;
    }
}
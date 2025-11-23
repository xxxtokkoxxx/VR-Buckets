namespace _VRBuckets.CodeBase.GamePlay.Data
{
    public interface IGameResultsContainer
    {
        void SetGameResults(GameResults gameResults);
        GameResults GetGameResults();
    }
}
using System.Collections.Generic;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public interface IPlayersContainer
    {
        void AddPlayer(PlayerEntity playerEntity);
        void RemovePlayer(int id);
        PlayerEntity GetPlayer(int playerId);
        Dictionary<int, PlayerEntity> GetPlayers();
        void ClearPlayers();
    }
}
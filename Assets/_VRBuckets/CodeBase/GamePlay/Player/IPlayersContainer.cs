using System;
using System.Collections.Generic;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public interface IPlayersContainer
    {
        void AddPlayer(PlayerEntity playerEntity);
        PlayerEntity GetPlayer(Guid playerId);
        Dictionary<Guid, PlayerEntity> GetPlayers();
        void ClearPlayers();
    }
}
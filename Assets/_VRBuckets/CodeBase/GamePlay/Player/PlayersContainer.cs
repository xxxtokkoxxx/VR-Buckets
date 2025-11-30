using System.Collections.Generic;
using System.IO;
using _VRBuckets.CodeBase.Logging;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public class PlayersContainer : IPlayersContainer
    {
        private Dictionary<int, PlayerEntity> _players = new();

        public void AddPlayer(PlayerEntity playerEntity)
        {
            if (_players.ContainsKey(playerEntity.Id))
            {
                AppLogger.LogError(LogCategory.Data, $"Player with ID {playerEntity.Id} is already added");
                return;
            }

            _players.Add(playerEntity.Id, playerEntity);
        }

        public void RemovePlayer(int id)
        {
            _players.Remove(id);
        }

        public PlayerEntity GetPlayer(int playerId)
        {
            bool playerExists = _players.TryGetValue(playerId, out PlayerEntity player);

            if (!playerExists)
            {
                throw new InvalidDataException($"There are no player with ID {playerId}");
            }

            return player;
        }

        public Dictionary<int, PlayerEntity> GetPlayers()
        {
            return _players;
        }

        public void ClearPlayers()
        {
            _players.Clear();
        }
    }
}
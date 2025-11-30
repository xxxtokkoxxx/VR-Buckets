using Fusion;

namespace _VRBuckets.CodeBase.Network.Messaging.NetworkEvents
{
    public struct PlayerJoinedEvent : INetworkEvent
    {
        public readonly PlayerRef Player;

        public PlayerJoinedEvent(PlayerRef player)
        {
            Player = player;
        }
    }

    public struct PlayerDisconnectedEvent : INetworkEvent
    {
        public readonly PlayerRef Player;

        public PlayerDisconnectedEvent(PlayerRef player)
        {
            Player = player;
        }
    }
}